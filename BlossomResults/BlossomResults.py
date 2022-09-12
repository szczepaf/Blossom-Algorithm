import networkx as nx
import matplotlib.pyplot as plt
from networkx.algorithms.shortest_paths import weighted


     
def main():
    G = nx.Graph()

    filename = "output.txt"
    with open(filename) as file:
        lines = file.readlines()
        lines = [line.rstrip() for line in lines]

    for strEdge in lines:
        
             if strEdge.startswith("m"):
                color = 'r'
                strEdge = strEdge.replace("m", "")
             else:
                color = 'b'
             strEdge = strEdge.replace("<","")
             strEdge = strEdge.replace(">","")
             nodes = strEdge.split(',')
             print(nodes)
             G.add_edge(nodes[0],nodes[1],color=color)
             
             
    colors = nx.get_edge_attributes(G,'color').values()
    pos = nx.spring_layout(G)


    nx.draw(G, pos, edge_color=colors)
    plt.show()  

    

main()
     
