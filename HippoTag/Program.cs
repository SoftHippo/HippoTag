using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace HippoTag
{
	class Program
	{
		static readonly string filename = "tags.txt";
		static void Main(string[] args)
		{
			HippoTag ht = new HippoTag();
			ht.ReadFromFile(filename);

			// TEST SECTION
			List<string> testWords = new List<string>() { "london", "trains" };
			List<Tag> selectedTags = new List<Tag>();

			foreach(var word in testWords)
			{
				foreach(Tag t in ht.CategoryTags[word])
				{
					selectedTags.Add(t);
				}
			}
			// sort by cat. count
			selectedTags.Sort((x, y) => -x.Categories.Count.CompareTo(y.Categories.Count));

			foreach(Tag t in selectedTags)
			{
				Console.WriteLine($"{t.Name}\t\t\t{t.Categories.Count}");
			}

			while(true)
			{
				Console.Write("> ");
				var input = Console.ReadLine();
				
				if(input == "exit")
				{
					break;
				}

				if(!ht.CategoryTags.ContainsKey(input))
				{
					Console.WriteLine("invalid category");
					continue;
				}

				foreach (Tag tag in ht.CategoryTags[input])
				{
					Console.WriteLine(tag.Name + "\t" + tag.FileLineNumber);
				}
				Console.WriteLine();
			}
		}
	}
}
