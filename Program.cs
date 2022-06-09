using static System.Console;
using static System.Math;


class AVLTree<T> where T : IComparable<T>{
    public class Node{
        public T val;
        public int height;
        public Node? parent;
        public Node? left;
        public Node? right;

        public Node(T val){
            this.val = val;
            height = 0;
            parent = null;
            left = null;
            right = null;
        }
    }

    public Node? root;
    public int num_nodes;
    public AVLTree(){
        root = null;
        num_nodes = 0;
    }

    public int node_height(Node? node){
        return node == null ? -1 : node.height;
    }

    public int calculate_balance(Node node){
        // calculate the balance factor for node and return
        // the difference between the height of the left subtree and that of the right subtree of the node
        if (node == null)
            return 0;

        return node_height(node.left) - node_height(node.right);
    }

    public Node minimum(Node n) {
        // find the minimum node within the avl tree
        while(n.left != null){
            n = n.left;
        }
        return n;
    }

    public Node maximum(Node n) {
        // find the maximum value within the avl tree
        while(n.right != null){
            n = n.right;
        }
        return n;
    }

    public bool find(T val){
        // return true if the value is found in the tree
        if (root == null){
            return false;
        }

        Node? node = root!;
        while (node != null){
            // if val is greater than node go right, else if left, else found
            if (val.CompareTo(node.val) > 0){
                node = node.right;
            } else if (val.CompareTo(node.val) < 0){
                node = node.left;
            } else if (val.CompareTo(node.val) == 0){
                return true;
            } else {
                return false;
            }
        }
        return false;
    }

    private void right_rotate(Node x) {
        // separate the right(parent) and left(child) nodes and make the L node the parent and the R node the child (CW rotation)
        //       x 
        //     y   c
        //    a b

        // move b up to be the left node of x
        Node y = x.left!;
        x.left = y.right;

        // set b's parent to x
        if (y.right != null) {
            y.right.parent = x;
        }
        // give x's parent to y 
        y.parent = x.parent;
        // if x is the root then make y the root
        if (x.parent == null) {
            this.root = y;
        }
        // assign y to be the left/right node of x's parent 
        else if (x == x.parent.left) {
            x.parent.left = y;
        }
        else if (x == x.parent.right) {
            x.parent.right = y;
        }

        // set x to be the right of y; and y to be the parent of x
        y.right = x;
        x.parent = y;

        // set node heights
        x.height = 1 + Max(node_height(x.left), node_height(x.right));
        y.height = 1 + Max(node_height(y.left), node_height(y.right));
    }

    private void left_rotate(Node x) {
        // same idea as for the right rotate except it is mirrored (CCW rotation)
        Node y = x.right!;
        x.right = y.left;

        if (y.left != null) {
            y.left.parent = x;
        }
        y.parent = x.parent;
        if (x.parent == null) {
            this.root = y;
        } else if (x == x.parent.left) {
            x.parent.left = y;
        }
        else {
            x.parent.right = y;
        }

        y.left = x;
        x.parent = y;

        x.height = 1 + Max(node_height(x.left), node_height(x.right));
        y.height = 1 + Max(node_height(y.left), node_height(y.right));
    }

    public void insert(T val){
        // This method inserts a value and balances the tree
        // There are four options for rebalancing the tree that could occur 
        Node? temp_node = root;
        Node n = new Node(val);
        Node? y = null;

        // cycle through to find location for insert
        while (temp_node != null) {
            y = temp_node;
            if (val.CompareTo(temp_node.val) < 0){
                temp_node = temp_node.left;
            } else if (val.CompareTo(temp_node.val) > 0){
                temp_node = temp_node.right;
            } else if (val.CompareTo(temp_node.val) == 0){
                return;
            }
        }

        // insert new node (y is the parent of the new node)
        n.parent = y;
        if (y == null) {
            root = n;
        } else if (val.CompareTo(y.val) < 0) {
            y.left = n;
        } else {
            y.right = n;
        }

        // meat and potatoes of insert
        Node z = n;
        while(y != null) {
            y.height = 1 + Max(node_height(y.left), node_height(y.right));
            Node x_node = y.parent!;

            if(calculate_balance(x_node) <= -2 || calculate_balance(x_node) >= 2) {
                if(y == x_node.right) {
                    // y is left child of x and z is left child of y - handles LL and L case
                    if (z == x_node.right.right) {
                        left_rotate(x_node);
                        // y is right child of x and z is left child of y - RL case
                    } else if (z == x_node.right.left) {
                        right_rotate(y);
                        left_rotate(x_node);
                    }
                } else if(y == x_node.left) {
                    // y is right child of x and z is right child of y - handles RR and R case
                    if(z == x_node.left.left) {
                        right_rotate(x_node);
                        // if y is left child of x and z is right child of y - LR case
                    } else if(z == x_node.left.right) {
                        left_rotate(y);
                        right_rotate(x_node);
                    }
                }
                break;
            }
            y = y.parent;
            z = z.parent!;
        }
        num_nodes += 1;
    }

    public int count_nodes(){
        /// This method returns the number of nodes currently in the tree
        return num_nodes;
    }

    public void print(Node? node) {
        // Pass a node to print values and smbols of wether L or R of node, prints a very rough tree best for small test cases
        if (node == null){
            WriteLine("empty tree!");
        } else {
            WriteLine($"{node.val}");

            if (node.right != null){
                WriteLine("\\");
                print(node.right);
            }
            if (node.left != null){
                WriteLine("/");
                print(node.left);
            }
        }
    }

    public void ordered_print(Node n){
        if (n != null){
            ordered_print(n.left);
            Write($"{n.val} ");
            ordered_print(n.right);
        }
    }

    private void move_up(Node u, Node v) {
        // helper method to rearrange nodes
        if (u.parent == null){
            // here u is root
            root = v;
        } else if (u == u.parent.left){
            // here u is left child
            u.parent.left = v;
        } else {
            // here u is right child
            u.parent.right = v;
        }

        if (v != null) {
            v.parent = u.parent;
        }
    }

    private void delete_rebalance(Node node) {
        // balancing method for deletion
        Node? p = node;

        while (p != null) {
            p.height = 1 + Max(node_height(p.left), node_height(p.right));
            // parent of the parent is unbalanced 
            if (calculate_balance(p) <= -2 || calculate_balance(p) >= 2) {
                Node x_node, y, z;
                x_node = p;

                // here the taller child of x is y
                if (node_height(x_node.left) > node_height(x_node.right)) {
                    y = x_node.left;
                } else {
                    y = x_node.right;
                }

                // here the taller child of y is z
                if (node_height(y.left) > node_height(y.right)) {
                    z = y.left;
                } else if (node_height(y.left) < node_height(y.right)) {
                    z = y.right;
                } else {
                    // here they are the same height, take a single rotation
                    if (y == x_node.left) {
                        z = y.left;
                    } else {
                        z = y.right;
                    }
                }

                if(y == x_node.right) {
                    // y is left child of x and z is left child of y - handles LL and L case
                    if (z == x_node.right.right) {
                        left_rotate(x_node);
                    // y is right child of x and z is left child of y - RL case
                    } else if (z == x_node.right.left) {
                        right_rotate(y);
                        left_rotate(x_node);
                    }
                } else if(y == x_node.left) {
                    // y is right child of x and z is right child of y - handles RR and R case
                    if(z == x_node.left.left) {
                        right_rotate(x_node);
                    // if y is left child of x and z is right child of y - LR case
                    } else if(z == x_node.left.right) {
                        left_rotate(y);
                        right_rotate(x_node);
                    }
                }
            }
            p = p.parent;
        }
    }


    public bool delete_node(T val) {
        // delete main driver function
        if (root == null) {
            return false;
        }

        Node? n = root!;
        Node? z = null;
        // find the node to delete
        while (n != null){
            if (val.CompareTo(n.val) > 0){
                n = n.right;
            } else if (val.CompareTo(n.val) < 0){
                n = n.left;
            } else if (val.CompareTo(n.val) == 0){
                z = n;
                break;
            } else {
                return false;
            }
        }

        // if there is no left subtree to the deleted node then 
        // move the right subtree up and reblance
        if (z.left == null) {
            move_up(z, z.right);
            if (z.right != null) {
                delete_rebalance(z.right);
            }
        // if there is no right subtree to the deleted node then 
        // move the left subtree up and reblance
        } else if (z.right == null) {
            move_up(z, z.left);
            if (z.left != null) {
                delete_rebalance(z.left);
            }
        } else {
            // two child node case, get min node in right subtree and move up
            // set right and left child, rebalance
            Node y = minimum(z.right);
            if (y.parent != z) {
                move_up(y, y.right);
                y.right = z.right;
                y.right.parent = y;
            }

            move_up(z, y);
            y.left = z.left;
            y.left.parent = y;
            if (y != null) {
                delete_rebalance(y);
            }
        }

        num_nodes -= 1;
        return true;
    }


}



class Top{
    static void Main(){
        //basic_I_O_test();
        medium_test(10_000_000);
    }

    static void basic_I_O_test(){
        var tree = new AVLTree<int>();
        tree.insert(10);
        tree.insert(15);
        tree.insert(13);
        tree.print(tree.root!);
        tree.ordered_print(tree.root!);
        WriteLine();
        WriteLine(tree.count_nodes());
        WriteLine(tree.node_height(tree.root));
        WriteLine("------");
        WriteLine(tree.delete_node(13));
        tree.print(tree.root!);
        WriteLine(tree.count_nodes());
        WriteLine(tree.node_height(tree.root));
        WriteLine("------");
        WriteLine(tree.delete_node(10));
        tree.print(tree.root!);
        WriteLine(tree.count_nodes());
        WriteLine(tree.node_height(tree.root));
        WriteLine("------");
        WriteLine(tree.delete_node(15));
        tree.print(tree.root!);
        WriteLine(tree.count_nodes());
        WriteLine(tree.node_height(tree.root));
    }

    static void medium_test(int number_of_items) {
        var tree = new AVLTree<int>();
        var values = new HashSet<int>();

        WriteLine($"inserting {number_of_items:n0} items...");
        
        for (int i = 1; i <= number_of_items; i++) {
            Random r = new Random();
            int rand = r.Next(0, 1_000_000_000); //for ints
            if (!values.Add(i))
            {
                i -= 1;
            }
        }

        foreach (var v in values) {
            tree.insert(v);
        }

        WriteLine($"number of items in tree: {tree.count_nodes():n0}");
        WriteLine($"tree height: {tree.node_height(tree.root)}");
        WriteLine($"tree root node value: {tree.root.val:n0}");
        WriteLine("deleting items...");

        foreach (var v in values) {
            tree.delete_node(v);
        }

        WriteLine($"number of items in tree: {tree.count_nodes()}");
    }

}