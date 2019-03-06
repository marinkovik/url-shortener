# Url Shortener using .NET framework

# A modern, minimalist, and lightweight URL shortener using ASP.NET Web Api.

# Short Demo



# RESTful API
POST api/url with form data "long_url" and it automatically generates short links using Base26 encoding and it comes into /?s=A (for example yourdomain.com/?s=A)
```
  {
    "longString": "google.com",
    "shortString": "A",
    "id": 1,
    "visits": 0,
    "visitsTime": []
  }
```
GET /api/url to get the data
```
[
  {
    "longString": "google.com",
    "shortString": "A",
    "id": 1,
    "visits": 3,
    "visitsTime": [ "02.03.2019 19:15:38", "02.03.2019 19:15:40", "02.03.2019 19:15:41" ]
  },
  {
    "longString": "https://www.youtube.com/watch?v=SYM-RJwSGQ8",
    "shortString": "B",
    "id": 2,
    "visits": 3,
    "visitsTime": [ "03.03.2019 14:28:22", "03.03.2019 14:28:29", "03.03.2019 14:28:41" ]
  },
  {
    "longString": "github.com",
    "shortString": "c",
    "id": 3,
    "visits": 2,
    "visitsTime": [ "06.03.2019 11:13:45", "06.03.2019 11:13:47" ]
  }
]
```

The similar services like this(pastebin, google shortener, etc) are using base52 or base62 to convert the IDs into the characters. You can override it in this part of the code
```
private static string ToBase26(int number)
        {
            var list = new LinkedList<int>();
            list.AddFirst((number - 1) % 26);
            while ((number = --number / 26 - 1) > 0)
            {
                list.AddFirst(number % 26);
            }
            return new string(list.Select(s => (char)(s + 65)).ToArray());
        }
```
# Visits statistics
You can add the plus sign ('+') at the end of the url so you can see number of clicks and the dates of the clicks



