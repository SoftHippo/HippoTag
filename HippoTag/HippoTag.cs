using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace HippoTag
{
	class HippoTag
	{
		public Dictionary<string, HashSet<Tag>> CategoryTags { get; set; } = new Dictionary<string, HashSet<Tag>>();
		public Dictionary<string, Tag> NameTags { get; set; } = new Dictionary<string, Tag>();
		public HashSet<Tag> AllTags { get; set; } = new HashSet<Tag>();

		public void ReadFromFile(string filename)
		{
			var TagRegex = new Regex(@"^#[A-Za-z0-9_]+$");
			var CategoryRegex = new Regex(@"^:([a-z]+)-?([a-z]+)?$");
			StreamReader reader = File.OpenText(filename);

			string line;
			string category = null;
			string categoryType = null;
			int lineNumber = 0;

			while ((line = reader.ReadLine()) != null)
			{
				lineNumber++;
				string[] lineSeparated = line.Split(null);

				foreach (string wordS in lineSeparated)
				{
					string word = wordS.Trim();
					if (word == "") { continue; }

					// ITS A TAG!
					if (TagRegex.IsMatch(word))
					{
						if (category is null) throw HippoTagException("No category for first tag");
						if (NameTags.ContainsKey(word))
						{
							var existingTag = NameTags[word];
							if (!existingTag.Categories.Contains(category)) 
							{
								existingTag.Categories.Add(category);
								CategoryTags[category].Add(existingTag);
							}
							continue;
						}

						Tag newTag = new Tag()
						{
							Categories = new List<string>() { category },
							FileLineNumber = lineNumber,
							Name = word,
							CategoryType = categoryType
						};
						if (CategoryTags[category].Contains(newTag))
						{
							Console.WriteLine($"WARNING: duplicate tag {newTag}");
							continue;
						}
						else
						{
							NameTags[word] = newTag;
							CategoryTags[category].Add(newTag);
							AllTags.Add(newTag);

							continue;
						}
					}
					
					// ITS A CATEGORY
					Match m = CategoryRegex.Match(word);	
					if (m.Success)
					{
						category = m.Groups[1].Value;
						categoryType = m.Groups[2].Value;

						CategoryTags[category] = new HashSet<Tag>(new TagComparer());
					}

					else
					{
						Console.WriteLine($"WARNING: invalid token: {word}");
					}
				}
			}

			private void AddTag()
		}

		private static Exception HippoTagException(string v)
		{
			throw new NotImplementedException();
		}
	}
}
