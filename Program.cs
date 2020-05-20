using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ARGraph;

namespace GraphTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("TESTING NODE BASED GRAPH");
            //Testing_NodeBased();
            //Console.WriteLine();
            //Console.WriteLine();
            //Console.WriteLine();
            Console.WriteLine("TESTING ADJACENCY LIST GRAPH");
            Testing_AdjacencyList();
        }

        static public void Testing_NodeBased()
        {

            // Test CSC382Graph_NodeBased Constructor(s)
            CSC382Graph_Vertex<string> baseNode = new CSC382Graph_Vertex<string>("The answer is: ");
            CSC382Graph_NodeBased<string> stringGraph = new CSC382Graph_NodeBased<string> (baseNode);

            // Test creating lots of nodes
            string[] lines = File.ReadAllLines("num100.dat");


            Console.WriteLine("Adding vertex nodes to the graph");
            foreach (var line in lines)
            {
                bool exists = false;
                CSC382Graph_Vertex<string> temp = new CSC382Graph_Vertex<string>(line);
                foreach (var item in stringGraph.Graph)
                {
                    //make sure there aren't duplicates in the graph
                    if (item.GetData().Equals(temp.GetData()))
                    {
                        exists = true;
                        break;
                    }
                    
                }

                if (!exists)
                {
                    stringGraph.Insert(temp);
                }
                

            }

            // Test functions of the CSC382Graph_NodeBased class and affiliated classes

            //test size: should be 101 -- 1 for the baseNode, plust one for the 100 lines that are in the file we read in
            Console.WriteLine("The length of the string Graph is: " + stringGraph.Size());
            Console.WriteLine();

            

            CSC382Graph_Vertex<string> fortyTwo = new CSC382Graph_Vertex<string>("42");
            bool found = stringGraph.FindVertex(fortyTwo) != default;
            Console.WriteLine("Let's try and find the node containing \"42\". ");
            Console.WriteLine("Did we find it? " + stringGraph.Graph.First().GetData() +   found );
            if (found)
            {
                Console.WriteLine("Weird, \"42\" is not supposed to be in the Graph");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("That's ok. I wasn't expecting it to be in there.");
                Console.WriteLine();
            }


            //Now we find one that is in the list
            Console.WriteLine("How about the node containing \"47\". ");
            CSC382Graph_Vertex<string> fortySeven = stringGraph.FindVertex("47");
            found = fortySeven != default;
            Console.WriteLine("Did we find it? " + stringGraph.Graph.First().GetData() + found);

            if (!found)
            {
                Console.WriteLine("Weird, \"47\" is supposed to be in the Graph");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Great!");
                Console.WriteLine();
            }

            Console.WriteLine( "Let's add \"42\" to the Graph with an edge connecting the two");
            stringGraph.Insert(fortyTwo);
            stringGraph.AddEdge(fortySeven, fortyTwo);
            Console.WriteLine("Is \"42\" connected to \"47\"?" + stringGraph.Graph.First().GetData() + stringGraph.IsEdge(fortySeven, fortyTwo));
            Console.WriteLine();
            Console.WriteLine("I know \"100\" is in the Graph.  Let's see if it is connected to \"47\"." );
            found = fortySeven.VertexConnected(stringGraph.FindVertex("100")) != default;
            Console.WriteLine(stringGraph.Graph.First().GetData() + found);
            if (!found)
            {
                Console.WriteLine("I wander why?");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Great!");
                Console.WriteLine();
            }

            found = fortySeven.VertexConnected(fortyTwo) != default;
            Console.WriteLine("Let's verify that \"42\" got added with an edge to \"47\". " + stringGraph.Graph.First().GetData() + found);
            Console.WriteLine();

            int edgeNum = 0;

            var current = stringGraph.Graph.First;
            
            while (current != null)
            {
                var compare = current.Next;

                while (compare != null)
                {
                    if (stringGraph.IsEdge(current.Value, compare.Value))
                    {
                        edgeNum++;
                    }
                    compare = compare.Next;
                }
                
                current = current.Next;
                
            }

            //at this point there should not be any edges except for the one between 42 and 47
            Console.WriteLine("How many edges do we have in the Graph?" + stringGraph.Graph.First().GetData() + edgeNum);

            Console.WriteLine();
            Console.WriteLine("Let's add some more edges so that all the nodes are connected");
            current = stringGraph.Graph.First;
            while (current != null)
            {
                if (current.Next != null)
                {
                    current.Value.AddEdge(current.Next.Value);
                    current.Next.Value.AddEdge(current.Value);

                    if (current.Next.Next !=null )
                    {
                        current.Value.AddEdge(current.Next.Next.Value);
                        current.Next.Next.Value.AddEdge(current.Value);
                    }
                    
                }
                

                current = current.Next;
            }
            Console.WriteLine();
            edgeNum = 0;
            current = stringGraph.Graph.First;

            while (current != null)
            {
                var compare = current.Next;

                while (compare != null)
                {
                    if (stringGraph.IsEdge(current.Value, compare.Value))
                    {
                        edgeNum++;
                    }
                    compare = compare.Next;
                }

                current = current.Next;

            }

            //at this point there should not be any edges except for the one between 42 and 47
            Console.WriteLine("How many edges do we have Now?" + stringGraph.Graph.First().GetData() + edgeNum);
            Console.WriteLine();

            Console.WriteLine("the current graph edges are: ");
            current = stringGraph.Graph.First;
            while (current != null)
            {
                foreach (var item in current.Value.Connected_vertices)
                {
                    Console.WriteLine(current.Value.GetData() + " -- " + item.GetData());
                }
                

                current = current.Next;
            }
            Console.WriteLine();
        }

        static public void Testing_AdjacencyList()
        {
            // Test CSC382Graph_AdjacencyList Constructor(s)
            CSC382Graph_AdjacencyList<int> intGraph = new CSC382Graph_AdjacencyList<int>();

            // Test functions of the CSC382Graph_AdjacencyList class
            


            string[] lines = File.ReadAllLines("num100.dat");
            
            // Test creating lots of nodes and edges
            Console.WriteLine("Adding vertex nodes to the graph");
            int prev = 0;
            foreach (var line in lines)
            {
                try
                {
                    int current = int.Parse(line);
                    
                    //intGraph.AddVertex(current);

                    LinkedList<int> curr = intGraph.AddVertex(current);
                    

                    if (prev != 0 && prev != current)
                    {
                        LinkedList<int> adj = intGraph.FindVertex(prev);
                        intGraph.AddEdge(adj, curr.First.Value);
                        intGraph.AddEdge(curr, adj.First.Value);
                        //intGraph.AddEdge(intGraph.FindVertex(prev), intGraph.FindVertex(current));
                    }

                    
                    
                    prev = current;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(line + ": " + ex.Message);
                    Console.WriteLine();

                }
            }
            

            //try to add edge with new data and no vertex given
            Console.WriteLine("try to add edge with new data 42 and no vertex given");
            try
            {
                intGraph.AddEdge(null, 42);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message + " Adding it to the graph.");
                Console.WriteLine();
            }
            

            //try to add edge with new data and empty vertex
            Console.WriteLine("try to add edge with new data 99 and empty vertex");
            LinkedList<int> newVertex = new LinkedList<int>();
            try
            {
                intGraph.AddEdge(newVertex, 99);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " Adding it to the graph");
                Console.WriteLine();
            }
            


            
            

            //print graph
            Console.WriteLine("Printing graph");
            
            int size = intGraph.GraphSize();
            Console.WriteLine("Number of nodes: " + size);
            int edges = intGraph.NumberOfEdges();
            Console.WriteLine("Number of edges: " + edges);
            Console.WriteLine();
            Console.WriteLine(intGraph.PrintAdjacencyList());

            

            Console.WriteLine();
            Console.ReadLine();
           
        }
    }
}
