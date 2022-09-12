#Edmonds Blossom Algorithm, Frantisek Szczepanik, MFF UK 2022
##szczepanik@centrum.cz

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



