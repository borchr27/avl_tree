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

    private int node_height(Node? node){
        return node == null ? -1 : node.height;
    }

    public int calculate_balance(Node node){
        // calculate the balance factor for node and return
        // the difference between the height of the left subtree and that of the right subtree of the node
        if (node == null)
            return 0;
        
        return node_height(node.left) - node_height(node.right);
    }

    public T minimum() {
        // find the minimum value within the avl tree
        Node n = root!;
        while(n.left != null){
            n = n.left;
        }
        return n.val;
    }

    public T maximum() {
        // find the maximum value within the avl tree
        Node n = root!;
        while(n.right != null){
            n = n.right;
        }
        return n.val;
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

    public void rightRotate(Node x) {
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

    public void leftRotate(Node x) {
        // same idea as for the right rotate except it is mirrored (CCW rotation)
        Node y = x.right!;
        x.right = y.left;

        if (y.left != null) {
            y.left.parent = x;
        }
        y.parent = x.parent;
        if (x.parent == null) {
            this.root = y;
        } else if (x == x.parent.right) {
            x.parent.right = y;
        } else if (x == x.parent.left) {
            x.parent.left = y;
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
            } else {
                temp_node = temp_node.right;
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
                        leftRotate(x_node);
                    // y is right child of x and z is left child of y - RL case
                    } else if (z == x_node.right.left) { 
                        rightRotate(y);
                        leftRotate(x_node);
                    }
                } else if(y == x_node.left) {
                    // y is right child of x and z is right child of y - handles RR and R case
                    if(z == x_node.left.left) {
                        rightRotate(x_node);
                    // if y is left child of x and z is right child of y - LR case
                    } else if(z == x_node.left.right) {
                        leftRotate(y);
                        rightRotate(x_node);
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

    public void print(Node node){
        // Pass a node to print values and smbols of wether L or R of node, prints a very rough tree best for small test cases
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

    // TODO Delete Method

}



class Top{
    static void Main(){
        var tree = new AVLTree<int>();
        tree.insert(10);
        tree.insert(15);
        tree.insert(13);
        tree.print(tree.root!);
        WriteLine(tree.count_nodes());
        WriteLine(tree.root!.height);
    }
}