using System;

namespace TreeDataStructure
{
    class Program
    {
        static void Main(string[] args)
        {
            BinarySearchTree searchTree = new BinarySearchTree(15);
            searchTree.Insert(10);
            searchTree.Insert(20);
            searchTree.Insert(8);
            searchTree.Insert(12);
            searchTree.Insert(17);
            searchTree.Insert(19);
            searchTree.Insert(25);

            Console.WriteLine(searchTree.Remove(17,null));
            #region Note.
            /*
                   // Root.
                   Note<string> rootTree = new Note<string>("a");
                   rootTree.InsertLeft("b");
                   rootTree.InsertRight("c");

                   Note<string> bNote = rootTree.LeftChild;
                   bNote.InsertRight("d");

                   Note<string> cNote = rootTree.RightChild;
                   cNote.InsertLeft("e");
                   cNote.InsertRight("f");

                   rootTree.PostOrder();
                   */
            #endregion
        }
    }

    public class BinarySearchTree
    {
        public BinarySearchTree(int value)
        {
            this.Value = value;
            this.LeftChild = null;
            this.RightChild = null;
        }

        public void Insert(int data)
        {
            // Is data < the father itself and the father already have a child.
            if (data < this.Value && this.LeftChild != null)
                this.LeftChild.Insert(data);
            // The left child doesn't have yet.
            else if (data < this.Value)
                this.LeftChild = new BinarySearchTree(data);
            else if (data > this.Value && this.RightChild != null)
                this.RightChild.Insert(data);
            // The left child doesn't have yet.
            else if (data > this.Value)
                this.RightChild = new BinarySearchTree(data);
            // Special case data = this.Value. Simple interate step 1, InSert it to the left subtree.
            else
                this.LeftChild.Insert(Value);
        }

        public bool Search(int data)
        {
            if (data < this.Value && this.LeftChild != null)
                return this.LeftChild.Search(data);
            else if (data > this.Value && this.RightChild != null)
                return this.RightChild.Search(data);

            return data == this.Value;
        }

        public bool Remove(int data, BinarySearchTree parent)
        {
            // data < this.Value then turn left.
            if (data < this.Value && this.LeftChild != null)
                return this.LeftChild.Remove(data, this);
            else if (data < this.Value)
                return false;
            // data > this.Value we turn right.
            else if (data > this.Value && this.RightChild != null)
                return this.RightChild.Remove(data, this);
            else if (data > this.Value)
                return false;

            // Case 0: A node with no children(leaf note). 
            if (this.LeftChild == null && this.RightChild == null && this == parent.LeftChild)
            {
                parent.LeftChild = null;
                this.Clear();
            }
            else if (this.LeftChild == null && this.RightChild == null && this == parent.RightChild)
            {
                parent.RightChild = null;
                this.Clear();
            }
            // Case 1: A node with just one child (left or right child).
            else if (this.LeftChild != null && this.RightChild == null && this == parent.LeftChild)
            {
                parent.LeftChild = this.LeftChild;
                this.Clear();
            }
            else if (this.LeftChild != null && this.RightChild == null && this == parent.RightChild)
            {
                parent.RightChild = this.LeftChild;
                this.Clear();
            }
            // Case 2: A node with two children.
            else if (this.LeftChild != null && this.RightChild != null && this == parent.LeftChild)
            {
                parent.LeftChild = this.RightChild;
                this.Clear();
            }
            else if (this.LeftChild != null && this.RightChild != null && this == parent.RightChild)
            {
                parent.RightChild = this.RightChild;
                this.Clear();
            }
            else
            {
                this.Value = this.RightChild.FindMinimunValue();
                this.RightChild.Remove(this.Value, this);
            }
            return true;
        }

        public int FindMinimunValue()
        {
            return (this.LeftChild != null) ? this.LeftChild.FindMinimunValue() : this.Value;
        }

        public void Clear()
        {
            this.LeftChild = null;
            this.RightChild = null;
            this.Value = 0;
        }

        public int Value { get; private set; }
        public BinarySearchTree LeftChild { get; private set; }
        public BinarySearchTree RightChild { get; private set; }
    }

    public class Note<T>
    {
        public Note(T value)
        {
            data = value;
            LeftChild = null;
            RightChild = null;
        }

        // Insert into left branch.
        public void InsertLeft(T data)
        {
            // If the current node doesn’t have a left child.
            if (LeftChild == null)
                LeftChild = new Note<T>(data);
            else
            {
                // we create a new node and put it in the current left child‘s place.
                // allocate this left child node to the new node‘s left child.
                Note<T> newNote = new Note<T>(data);
                newNote.LeftChild = LeftChild;
                LeftChild = newNote;
            }

        }
        // Insert into right branches.
        public void InsertRight(T data)
        {
            // If the current node doesn't have a right child.
            if (RightChild == null)
                RightChild = new Note<T>(data);
            else
            {
                // we create a new node and put it in the current right child‘s place.
                // allocate this right child node to the new node‘s right child.
                Note<T> newNote = new Note<T>(data);
                newNote.RightChild = this.RightChild;
                this.RightChild = newNote;
            }
        }

        public void PreOrder()
        {
            Console.WriteLine(this.data);
            if (this.leftChild != null)
                leftChild.PreOrder();

            if (this.rightChild != null)
                rightChild.PreOrder();
        }

        public void InOrder()
        {
            if (this.leftChild != null)
                this.leftChild.InOrder();

            Console.WriteLine(this.data);

            if (this.rightChild != null)
                this.rightChild.InOrder();
        }

        public void PostOrder()
        {
            if (this.leftChild != null)
                this.leftChild.PostOrder();

            if (this.rightChild != null)
                this.rightChild.PostOrder();

            Console.WriteLine(this.data);
        }

        public void BreadthFirstSearch()
        {
            Note<T> queue = Queue();
            queue.Put(this);

            while (!queue.Empty())
            {
                Note<T> currentNote = queue.Get();
                Console.WriteLine(currentNote.data);

                if (currentNote.leftChild != null)
                    queue.Put(currentNote.leftChild);

                if (currentNote.rightChild != null)
                    queue.Put(currentNote.rightChild);
            }

        }

        private Note<T> Get()
        {
            throw new NotImplementedException();
        }

        private bool Empty()
        {
            throw new NotImplementedException();
        }

        private void Put(Note<T> note)
        {
            throw new NotImplementedException();
        }

        private Note<T> Queue()
        {
            throw new NotImplementedException();
        }

        // Binary Tree.
        public T data;
        private Note<T> leftChild;
        private Note<T> rightChild;

        public Note<T> LeftChild { get => this.leftChild; private set => this.leftChild = value; }
        public Note<T> RightChild { get => this.rightChild; private set => this.rightChild = value; }
    }
}
