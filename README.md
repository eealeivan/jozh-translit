# jozh-translit [![NuGet Version](http://img.shields.io/nuget/v/jozh-translit.svg?style=flat)](https://www.nuget.org/packages/jozh-translit/)
`jozh-translit` is a .NET [transliteration](https://en.wikipedia.org/wiki/Transliteration) library. Can be used to transliterate static texts and also to work in "live" mode. Supported platforms: NET 3.5, NET 4, NET 4.5, .NET Portable (.NET 4 / .NET 4.5, Silverlight 5, Windows 8, Windows Phone 8.1, Windows Phone Silverlight 8).

## Sample usage
```csharp
var t = new Transliterator(EnRu.MapJson);
var res = t.Transliterate("Shhuchka i jozh bol'shie druz'ja!");
// res: Щучка и ёж большие друзья!
```

## Supported transliteration maps
Currently the only built-in map is [´en->ru´](https://github.com/eealeivan/jozh-translit/blob/master/Src/JoZhTranslit/TransliterationMaps/EnRu.cs), but you can easily provide your own maps. Map should be a JSON object with following structure:
```json
{
	"А": ["A"],
	"а": ["a"],
	"Ё": ["Jo", "Yo"],
	"ё": ["jo", "yo"],
	"Щ": ["Shh", "W"],
	"щ": ["shh", "w"],
	"Ъ": ["##"],
	"ъ": ["#"]
}
```
Please note that you need to provide graphemes for both upper case and lower case.

## Static transliteration
Used when you have a text and you need to transliterate it. For such operation you will need to create an instance of [`Transliterator`](https://github.com/eealeivan/jozh-translit/blob/master/Src/JoZhTranslit/Transliterator.cs) class and call `Transliterate` method. 

## Live transliteration
Can be connected with some input field to simulate live typing. Use [`TransliteratorLive`](https://github.com/eealeivan/jozh-translit/blob/master/Src/JoZhTranslit/TransliteratorLive.cs) class. It has `AddChar` method that returns [`AddCharResult`](https://github.com/eealeivan/jozh-translit/blob/master/Src/JoZhTranslit/AddCharResult.cs) class which contains found grapheme and status ("instruction" of what to do with added char). `TransliteratorLive` saves previously added chars that allows to enter complex grapehemes such as `ch`, `shh` etc. 

Lets check the next example:
```csharp
var t = new TransliteratorLive(EnRu.MapJson);
AddCharResult res1 = t.AddChar('C');
AddCharResult res2 = t.AddChar('h');
AddCharResult res3 = t.AddChar('a');
AddCharResult res4 = t.AddChar('j');
AddCharResult res5 = t.AddChar('!');
```
Now let's see what is returned for every call of `AddChar` method:

Char | Status | Grapheme | Explanation
------------- | ------ | -------- | -----------
**'C'** | NewGrapheme | **"Ц"** | **"C"** is equal to **"Ц"**.
**'h'** | SubstitutePreviousGrapheme | **"Ч"** | **"Ch"** is equal to **"Ч"**. Status says that previous grapheme should be replaced
**'a'** | NewGrapheme | **"а"** | We can't find anything for **"Cha"** so try **"a"** and find **"а"**.
**'j'** | NewGrapheme | **"й"** | We can't find anything for **"aj"** so try **"j"** and find **"й"**.
**'!'** | NoGraphemeFound | **NULL** | We can't find anything for **"j!"** and **"!"** so status says that grapheme was not found and grapheme itslef is null.

There is also a `Reset` method. It will clear previously eneted chars in `TransliteratorLive`, so next char that you enter will be processed without a history:
```csharp
var t = new TransliteratorLive(EnRu.MapJson);
AddCharResult res1 = t.AddChar('C');
t.Reset();
AddCharResult res2 = t.AddChar('h');
```
Explanation:

Char | Status | Grapheme | Explanation
------------- | ------ | -------- | -----------
**'C'** | NewGrapheme | **"Ц"** | **"C"** is equal to **"Ц"**.
**'h'** | NewGrapheme | **"х"** | Reset was called. **"h"** is equal to **"х"**.
