namespace Priority_Queue
{
    public class NodoColaPrioridadBase
    {
        /// <summary>
        /// PRIORIDAD DEL NODO EN LA COLA
        /// Debe establecerse antes de encolar (idealmente en el constructor).
        /// No debe modificarse una vez ha sido encolado. usar metodo correspondiente (decrementarPrioridad()).
        /// </summary>
        public float prioridad
        {
            get;
            protected internal set;
        }

        /// <summary>
        /// Posicion del nodo en la cola
        /// </summary>
        public int indiceCola
        {
            get;
            internal set;
        }
    }
}