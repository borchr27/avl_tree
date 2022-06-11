using static System.Console;
using System.Diagnostics;



class Top{
    static void Main(){
        // WriteLine("Basic Test for BST");
        // basic_test(new BinaryTree());
        // WriteLine("Basic Test for AVL");
        // basic_test(new AVLTree<int>());

        // WriteLine("Medium Random Test for BST");
        // medium_rand_test(new BinaryTree(), 1_000_000);
        // WriteLine("Medium Random Test for AVL");
        // medium_rand_test(new AVLTree<int>(), 1_000_000);

        WriteLine("Medium Increasing Test for BST");
        medium_inc_test(new BinaryTree(), 10_000);
        WriteLine("Medium Increasing Test for AVL");
        medium_inc_test(new AVLTree<int>(), 10_000);

    }

    static void basic_test(ITree<int> tree) {
        int[] vals = {10, 15, 13, 16, 20, 25, 33, 47};

        foreach (var v in vals) {
            WriteLine($"\t Inserting {v}... ");
            tree.add(v);
        }

        WriteLine($"\t Number of items in tree: {tree.node_count():n0}");
        WriteLine($"\t Tree height: {tree.get_tree_depth():n0}");

        foreach (var v in vals) {
            WriteLine($"\t Deleting {v}... ");
            tree.remove(v);
        }
        WriteLine($"\t Number of items in tree: {tree.node_count():n0}");
    }

    static void medium_rand_test(ITree<int> tree, int number_of_items){
        Stopwatch stopwatch = new Stopwatch();
        var values = new HashSet<int>();

        Random r = new Random();
        for (int i = 1; i <= number_of_items; i++) {
            int rand = r.Next(0, 1_000_000_000); //for ints
            while (!values.Add(rand)) {
                rand -= 1;
            }
        }

        Write($"\t Inserting {number_of_items:n0} items... ");
        stopwatch.Start();
        foreach (var v in values) {
            tree.add(v);
        }
        stopwatch.Stop();
        WriteLine("DONE");
        WriteLine($"\t Elapsed time for insert is {stopwatch.ElapsedMilliseconds} ms");

        Write($"\t Finding all values... ");
        foreach (var v in values) {
            if (tree.find(v) == false){
                WriteLine($"\t ERROR: {v} could not be found");
            }
        }
        WriteLine("DONE");

        WriteLine($"\t Number of items in tree: {tree.node_count():n0}");
        WriteLine($"\t Tree height: {tree.get_tree_depth():n0}");
        
        Write("\t Deleting items... ");
        foreach (var v in values) {
            tree.remove(v);
        }
        WriteLine("DONE");

        WriteLine($"\t Number of items in tree: {tree.node_count()}");
        WriteLine("Test Complete!");
    }


    static void medium_inc_test(ITree<int> tree, int number_of_items){
        Stopwatch stopwatch = new Stopwatch();
        var values = new int[number_of_items];

        for (int i = 1; i <= number_of_items; i++) {
            values[i-1] = i;
        }

        Write($"\t Inserting {number_of_items:n0} items... ");
        stopwatch.Start();
        foreach (var v in values) {
            tree.add(v);
        }
        stopwatch.Stop();
        WriteLine("DONE");
        WriteLine($"\t Elapsed time for insert is {stopwatch.ElapsedMilliseconds} ms");

        Write($"\t Finding all values... ");
        foreach (var v in values) {
            if (tree.find(v) == false){
                WriteLine($"\t ERROR: {v} could not be found");
            }
        }
        WriteLine("DONE");

        values.Reverse();

        WriteLine($"\t Number of items in tree: {tree.node_count():n0}");
        WriteLine($"\t Tree height: {tree.get_tree_depth():n0}");
        
        Write("\t Deleting items... ");
        foreach (var v in values) {
            tree.remove(v);
        }
        WriteLine("DONE");

        WriteLine($"\t Number of items in tree: {tree.node_count()}");
        WriteLine("Test Complete!");
    }
}