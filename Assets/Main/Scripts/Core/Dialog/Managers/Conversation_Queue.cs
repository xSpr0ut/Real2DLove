<<<<<<< HEAD
using System.Collections.Generic;

namespace Dialog{

    public class Conversation_Queue{
        
        private Queue<Conversation> conversationQueue = new Queue<Conversation>();
        public Conversation top => conversationQueue.Peek();

        public void Enqueue(Conversation conversation) => conversationQueue.Enqueue(conversation);

        public void Enqueue_Priority(Conversation conversation){

            Queue<Conversation> queue = new Queue<Conversation>();
            queue.Enqueue(conversation);

            while(conversationQueue.Count > 0){

                queue.Enqueue(conversationQueue.Dequeue());

            }

            conversationQueue = queue;

        }

        public void Dequeue(){

            if(conversationQueue.Count > 0){

                conversationQueue.Dequeue();

            }

        }

        public bool isEmpty() => conversationQueue.Count == 0;

        public void Clear() => conversationQueue.Clear();

        public Conversation[] Get_Readonly() => conversationQueue.ToArray();

    }

}
=======
using System.Collections.Generic;

namespace Dialog{

    public class Conversation_Queue{
        
        private Queue<Conversation> conversationQueue = new Queue<Conversation>();
        public Conversation top => conversationQueue.Peek();

        public void Enqueue(Conversation conversation) => conversationQueue.Enqueue(conversation);

        public void Enqueue_Priority(Conversation conversation){

            Queue<Conversation> queue = new Queue<Conversation>();
            queue.Enqueue(conversation);

            while(conversationQueue.Count > 0){

                queue.Enqueue(conversationQueue.Dequeue());

            }

            conversationQueue = queue;

        }

        public void Dequeue(){

            if(conversationQueue.Count > 0){

                conversationQueue.Dequeue();

            }

        }

        public bool isEmpty() => conversationQueue.Count == 0;

        public void Clear() => conversationQueue.Clear();

        public Conversation[] Get_Readonly() => conversationQueue.ToArray();

    }

}
>>>>>>> c8d4f422 (Whole Template)
