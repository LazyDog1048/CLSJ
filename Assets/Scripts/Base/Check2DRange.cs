using UnityEngine;
using UnityEngine.Events;

namespace plug
{
    // [RequireComponent(typeof(BoxCollider2D))]
    public class Check2DRange : MonoBehaviour
    {
        public UnityAction<Collider2D> triggerEnter { get; set; }
        public UnityAction<Collider2D> triggerExit { get; set; }
        public UnityAction<Collider2D> triggerStay { get; set; }
        
        
        public UnityEvent<Collider2D> triggerEnterEvent { get; set; }
        public UnityEvent<Collider2D> triggerExitEvent { get; set; }
        
        private Collider2D _collider2D;

        public bool isEnable
        {
            get => _collider2D.enabled;
            set => _collider2D.enabled = value;
        }
        
        private void Awake()
        {
            _collider2D = GetComponent<Collider2D>();
            triggerEnterEvent = new UnityEvent<Collider2D>();
            triggerExitEvent = new UnityEvent<Collider2D>();
        }

        public void Init(float range,UnityAction<Collider2D> enter,UnityAction<Collider2D> exit)
        {
            transform.localScale = Vector3.one * range;
            triggerEnter = enter;
            triggerExit = exit;
        }

        public void AddListener(UnityAction<Collider2D> enter,UnityAction<Collider2D> exit)
        {
            triggerEnterEvent.AddListener(enter);
            triggerExitEvent.AddListener(exit);
        }
        
        public void RemoveListener()
        {
            triggerEnterEvent = new UnityEvent<Collider2D>();
            triggerExitEvent = new UnityEvent<Collider2D>();
        }
        
        public void SetRange(float range)
        {
            transform.localScale = Vector3.one * range;
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            triggerEnter?.Invoke(other);
            triggerEnterEvent?.Invoke(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            triggerExit?.Invoke(other);
            triggerExitEvent?.Invoke(other);
        }
        
        private void OnTriggerStay2D(Collider2D other)
        {
            triggerStay?.Invoke(other);
        }
    }
    
}
