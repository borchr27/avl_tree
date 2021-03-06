// adapted from / courtesy of https://csharpexamples.com/c-binary-search-tree-implementation/

class Node
{
    public Node? LeftNode { get; set; }
    public Node? RightNode { get; set; }
    public int Data { get; set; }
}

class BinaryTree : ITree<int>
{
    public Node? Root;
    private int num_nodes;

    public BinaryTree()
    {
        num_nodes = 0;
        Root = null;
    }

    public int node_count(){
        return num_nodes;
    }

    public bool add(int value)
    {
        Node? before = null;
        Node? after = this.Root;

        while (after != null)
        {
            before = after;
            if (value < after.Data) //Is new node in left tree? 
                after = after.LeftNode;
            else if (value > after.Data) //Is new node in right tree?
                after = after.RightNode;
            else
            {
                //Exist same value
                return false;
            }
        }

        Node newNode = new Node();
        newNode.Data = value;

        if (this.Root == null) //Tree is empty
            this.Root = newNode;
        else
        {
            if (value < before!.Data)
                before.LeftNode = newNode;
            else
                before.RightNode = newNode;
        }
        num_nodes += 1;
        return true;
    }

    public bool find(int value)
    {
        return this.Find(value, this.Root) != null ? true : false;
    }

    public void remove(int value)
    {
        if (this.Root != null){
            this.Root = Remove(this.Root, value);
        }
    }

    private Node? Remove(Node? parent, int key)
    {
        if (parent == null) {
            return parent;
        }

        if (key < parent.Data)
        {
            parent.LeftNode = Remove(parent.LeftNode, key);
        }
        else if (key > parent.Data)
        {
            parent.RightNode = Remove(parent.RightNode, key);
        }

        // if value is same as parent's value, then this is the node to be deleted  
        else
        {
            
            // node with only one child or no child  
            if (parent.LeftNode == null) {
                num_nodes -= 1;
                return parent.RightNode;
            }
            else if (parent.RightNode == null) {
                num_nodes -= 1;
                return parent.LeftNode;
            }
            
            // node with two children: Get the inorder successor (smallest in the right subtree)  
            parent.Data = MinValue(parent.RightNode);

            // Delete the inorder successor  
            parent.RightNode = Remove(parent.RightNode, parent.Data);
        }
        return parent;
    }

    private int MinValue(Node node)
    {
        int minv = node.Data;

        while (node.LeftNode != null)
        {
            minv = node.LeftNode.Data;
            node = node.LeftNode;
        }

        return minv;
    }

    private Node? Find(int value, Node? parent)
    {
        if (parent != null)
        {
            if (value == parent.Data){
                 return parent;
            }

            if (value < parent.Data){
                return Find(value, parent.LeftNode);
            } else {
                return Find(value, parent.RightNode);
            }
        }

        return null;
    }

    public int get_tree_depth()
    {
        return this.Root == null ? 0 : this.get_tree_depth(this.Root);
    }

    private int get_tree_depth(Node? parent)
    {
        return parent == null ? 0 : Math.Max(get_tree_depth(parent.LeftNode), get_tree_depth(parent.RightNode)) + 1;
    }
}