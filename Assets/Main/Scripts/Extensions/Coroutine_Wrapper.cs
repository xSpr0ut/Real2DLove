<<<<<<< HEAD
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Commands{

    public class Coroutine_Wrapper{

        private MonoBehaviour owner;
        private Coroutine coroutine;
        public bool isDone = false;
        
        public Coroutine_Wrapper(MonoBehaviour owner, Coroutine coroutine){

            this.owner = owner;
            this.coroutine = coroutine;

        }

        public void Stop(){

            owner.StopCoroutine(coroutine);
            isDone = true;

        }

    }

}
=======
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Commands{

    public class Coroutine_Wrapper{

        private MonoBehaviour owner;
        private Coroutine coroutine;
        public bool isDone = false;
        
        public Coroutine_Wrapper(MonoBehaviour owner, Coroutine coroutine){

            this.owner = owner;
            this.coroutine = coroutine;

        }

        public void Stop(){

            owner.StopCoroutine(coroutine);
            isDone = true;

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
