using static System.Console;
using System;


class AVLTree<T> where T : IComparable<T>{
    public class Node{
        public T val;
        public int height;
        public Node? left;
        public Node? right;

        public Node(T val){
            this.val = val;
            left = null;
            right = null;
        }
    }

    public Node root;
    public AVLTree(T val){
        this.root = new Node(val);
    }

    public bool find(T val){
        if (root == null){
            return false;
        }

        Node node = root!;
        while (node != null){
            // if val is greater than node go right, else if left, else found
            if (val.CompareTo(node.val) > 0){
                if (node.right != null)
                    node = node.right;
            } else if (val.CompareTo(node.val) < 0){
                if (node.left != null)
                    node = node.left;
            } else if (val.CompareTo(node.val) == 0){
                return true;
            }
        }
        return false;
    }

    public int calculate_balance(Node node){
        // calculate the balnce factor for a given node
        if (node == null){
            return 0;
        } else {
            return Math.Max(calculate_balance(node.left), calculate_balance(node.right));
        }
    }

    public void insert(T val){
        if (root == null){
            root = new Node(val);
        }

        var node = root;
        while (node != null){
            if (val.CompareTo(node.val) > 0){
                if (node.right == null) {
                    node.right = new Node(val); 
                    return;
                }
                node = node.right;
            } else if (val.CompareTo(node.val) < 0) {
                if (node.left == null) {
                    node.left = new Node(val); 
                    return;
                }
                node = node.left;
            }
        }
    }

    public void print(Node node){
        // pass the root node to print initially

        WriteLine(node.val);

        if (node.right != null)
            print(node.right);
        if (node.left != null)
            print(node.left);
    }


}



class Top{
    static void Main(){
        var tree = new AVLTree<int>(3);
        tree.insert(1);
        tree.insert(4);
        tree.insert(0);
        tree.insert(2);
        tree.print(tree.root);
        WriteLine(tree.calculate_balance(tree.root));

    }
}