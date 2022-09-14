Edmonds Blossom Algorithm, Frantisek Szczepanik, MFF UK 2022
szczepanik@centrum.cz

This is the Edmonds Blossom Algorithm.
It is used for computing maximal matching on general graphs (unlike flow algorithms which work only on bipartite graphs).
More about Maximal Matching and Blossom Algorithm: https://en.wikipedia.org/wiki/Blossom_algorithm



USAGE:

Run main.bat from this folder. Firstly, the main C# code that computes the matching itself runs, it stores the results in a text file.
Then a python script using libraries networkx and matplotlib visualizes the result by drawing the paired edges.
The C# main program does not use any non-standard libaries.

You can choose from 6 premade representative graphs or choose a random graph generator.
For example, the graphs from Blossom.txt and DoubleBlossom.txt are such graphs that flow algorithms would not find the solution as the graphs are not bipartite.
7th choice is my own generator of random graphs. Unfortunately, there is a bug which I cannot solve in one of the cases of lifting the path and therefore sometimes the computation spirals into infinity.


If you want to overwrite for your custom input, the graphs are encoded like so:
[number of nodes]\n<edge1>\n<edge2>

where edge is encoded like so: <node from, node to>
Node indices are 0-based.
An example of a graph is this:
3
<0,1>
<1,2>



If there are any issues, contact me at szczepanik@centrum.cz.


