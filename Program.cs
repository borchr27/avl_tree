using static System.Console;
using System;


class AVLTree<T> where T : IComparable<T>{
    public class Node{
        public T val;
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
                    node=node.left;
            } else if (val.CompareTo(node.val) == 0){
                return true;
            }
        }
        return false;
    }

    public void insert(T val){
        if (root == null){
            root = new Node(val);
        }

        var node = root;
        while (node != null){
            if (val.CompareTo(node.val) > 0){
                node = node.right;
                if (node == null)
                    node = new Node(val); 
                    return;
            } else if (val.CompareTo(node.val) < 0) {
                node = node.left;
                if (node == null)
                    node = new Node(val); 
                    return;
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
        var tree = new AVLTree<int>(5);
        tree.insert(6);
        tree.print(tree.root);

    }
}