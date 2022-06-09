using static System.Console;
using System.Diagnostics;



class Top{
    static void Main(){
        // AVL_basic_test();
        // BST_basic_test();

        AVL_medium_test(1_000_000);
        BST_medium_test(1_000_000);

    }

    static void BST_basic_test(){
        var tree = new BinaryTree();
        WriteLine($"BST Basic Test");
        int[] vals = {10, 15, 13, 16, 20, 25, 33, 47};

        foreach (var v in vals) {
            WriteLine($"\t Inserting {v}...  ");
            tree.Add(v);
        }

        WriteLine($"\t Number of items in tree: {tree.NodeCount():n0}");
        WriteLine($"\t Tree height: {tree.GetTreeDepth()}");

        foreach (var v in vals) {
            WriteLine($"\t Deleting {v}... ");
            tree.Remove(v);
        }
        WriteLine($"\t Number of items in tree: {tree.NodeCount():n0}");
    }

    static void BST_medium_test(int number_of_items){
        Stopwatch stopwatch = new Stopwatch();
        WriteLine($"BST Medium Test");
        var tree = new BinaryTree();
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
            tree.Add(v);
        }
        stopwatch.Stop();
        WriteLine("DONE");
        WriteLine($"\t Elapsed time for insert is {stopwatch.ElapsedMilliseconds} ms");

        Write($"\t Finding all values... ");
        foreach (var v in values) {
            if (tree.Find(v) == null){
                WriteLine($"\t ERROR: {v} could not be found");
            }
        }
        WriteLine("DONE");

        WriteLine($"\t Number of items in tree: {tree.NodeCount():n0}");
        WriteLine($"\t Tree height: {tree.GetTreeDepth()}");
        WriteLine($"\t Tree root node value: {tree.Root.Data:n0}");
        
        Write("\t Deleting items... ");
        foreach (var v in values) {
            tree.Remove(v);
        }
        WriteLine("DONE");

        WriteLine($"\t Number of items in tree: {tree.NodeCount()}");
        WriteLine("Test Complete!");
    }

    static void AVL_basic_test(){
        var tree = new AVLTree<int>();
        WriteLine($"AVL Basic Test");
        int[] vals = {10, 15, 13, 16, 20, 25, 33, 47};

        foreach (var v in vals) {
            WriteLine($"\t inserting {v}... ");
            tree.insert(v);
        }

        WriteLine($"\t number of items in tree: {tree.count_nodes():n0}");
        WriteLine($"\t tree height: {tree.node_height(tree.root)}");

        foreach (var v in vals) {
            WriteLine($"\t deleting {v}... ");
            tree.delete_node(v);
        }

        WriteLine($"\t number of items in tree: {tree.count_nodes():n0}");
    }

    static void AVL_medium_test(int number_of_items) {
        Stopwatch stopwatch = new Stopwatch();
        WriteLine($"AVL Medium Test");
        var tree = new AVLTree<int>();
        var values = new HashSet<int>();

        Random r = new Random();
        for (int i = 1; i <= number_of_items; i++) {
            int rand = r.Next(0, 1_000_000_000); //for ints
            while (!values.Add(rand)) {
                rand -= 1;
            }
        }
        stopwatch.Start();
        Write($"\t Inserting {number_of_items:n0} items... ");
        foreach (var v in values) {
            tree.insert(v);
        }
        stopwatch.Stop();
        WriteLine("DONE");
        WriteLine($"\t Elapsed time for insert is {stopwatch.ElapsedMilliseconds} ms");

        Write($"\t Finding all values... ");
        foreach (var v in values) {
            if (!tree.find(v)){
                WriteLine($"\t ERROR: {v} could not be found");
            }
        }
        WriteLine("DONE");

        WriteLine($"\t Number of items in tree: {tree.count_nodes():n0}");
        WriteLine($"\t Tree height: {tree.node_height(tree.root)}");
        WriteLine($"\t Tree root node value: {tree.root.val:n0}");
        
        Write("\t Deleting items... ");
        foreach (var v in values) {
            tree.delete_node(v);
        }
        WriteLine("DONE");

        WriteLine($"\t Number of items in tree: {tree.count_nodes()}");
        WriteLine("Test Complete!");
    }
}