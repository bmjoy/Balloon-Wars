using UnityEngine;
using System.Collections;
namespace PawerPack_All
{

    public class Manager : MonoBehaviour
    {

        public int counter = 0;
        public float delay = 1f;
        public Animator[] Animators;
        // Use this for initialization
        void Start()
        {
            StartCoroutine(ChangeAnimation());
        }

        IEnumerator ChangeAnimation()
        {

            yield return new WaitForSeconds(delay);

            int maxAnimation = 2;
            counter++;
            if (counter > maxAnimation)
            {
                counter = 1;
            }


            if(counter==1)
            {
                foreach (Animator an in Animators)
                {
                    an.Play("Walk");
                    delay = 3;


                }
            }
            else if (counter == 2)
            {
                foreach (Animator an in Animators)
                {
                    an.Play("Idle");
                    delay =.5f;

                }
            }

            StartCoroutine(ChangeAnimation());

        }


     }




     
}

