# Url Shortener using .NET framework

# A modern, minimalist, and lightweight URL shortener using ASP.NET Web Api.

# RESTful API
POST api/url with form data "long_url" and it automatically generates short links using Base26 encoding.
```
  {
    "longString": "google.com",
    "shortString": "A",
    "id": 1,
    "visits": 0,
    "visitsTime": []
  }
```
