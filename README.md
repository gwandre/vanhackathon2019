# vanhackathon2019
Vanhackathon 2019 (São Paulo Recruiting Fair) - Challenge

## Introduction
As we know, it is a challenge to the companies to benchmark info about other companies performance indicators.
Sensei Labs as a leader in market with technologies to help companies accelerate growth and culture, and to orchestrate their proccesses, is providing a new "open data" feature to help the companies securely and anonymously share some of their performance indicators to get back the average of the same indicator who it was shared.

## Disclosure
All of this info and shared data are fictitious just for the "VanHackathon" Software Hackathon purposes. Nothing was provided from the referenced company.

 API URL: http://loremipsum.com.not.a.real.url/api

## Available Methods

### GET /innovation
Get market average "Innovation Rate" from stored data from the last moth, considering the current date

### GET/innovation/{businessNumber}
Get Innovation Rate from your company stored into database from the last moth, considering the current date

{businessNumber} in canadian format "999999999AA9999" (9 numbers, 2 letters and 4 numbers)

### POST
Store Innovation Data information from your company to share with the another companies, and get feed back about your innovation data for the year/month you are sending data

```JSON
{
	"businessNumber": "999999999LL0001",
	"year": 2019,
	"month": 3,
	"numberOfInnovations": 2,
	"numberOfProducts": 27
}
````