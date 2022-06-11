

public interface ITree<T>
{
    public bool find(T val);
    public bool add(T val);
    public void remove(T val);

    public int node_count();
    public int get_tree_depth();
}