using System.Dynamic;

var JSON = @"[
  {
    ""ProductId"": 101,
    ""Name"": ""Smartphone"",
    ""Price"": 699.99,
    ""Specs"": {
      ""ScreenSize"": ""6.1 inch"",
      ""Battery"": ""3000 mAh"",
      ""Camera"": ""12 MP""
    }
  },
  {
    ""ProductId"": 102,
    ""Name"": ""Laptop"",
    ""Price"": 1299.99,
    ""Specs"": {
      ""Processor"": ""Intel i7"",
      ""RAM"": ""16 GB"",
      ""Storage"": ""512 GB SSD""
    },
    ""Warranty"": ""2 years""
  },
  {
    ""ProductId"": 103,
    ""Name"": ""Wireless Earbuds"",
    ""Price"": 199.99,
    ""Color"": ""Black""
  }
]";

var products = new List<ExpandoObject>();
JSON = JSON.TrimEnd(']');
JSON = JSON.TrimStart('[');
var separatedJson = JSON.Split(',').ToList();

foreach (var item in separatedJson) Console.WriteLine(item);