# UrlHitCount

Build
---------
If you have .Dotnet Core installed, then you can run this via the Dotnet command in Powershell.

As an example: dotnet .\UrlHitCount.dll C:\Code\UrlHitCount\TestFiles\input.txt

Otherwise, I have also provided an .exe version in the Publish folder. This one is self-contained,
so it can be run without having .Dotnet Core installed.

Notes
---------
I did not add any external logging into this application. This is mostly because it's a fairly small app and I didn't want to bring in any extra packages if I could help it. Normally, I would bring in Serilog or something similar. Instead, I just decided to write to the console for this.

I didn't add any testing frameworks either. I still tried to break things out so that it wouldn't be too hard to add something like unit testing into the app. As it is, it's small enough that it's easy enough to test on its own.

For Big O notation, unfortunately, it's probably O(n^2) at worst. It's mostly because the display portion ends up going through the data using nested foreach loops.