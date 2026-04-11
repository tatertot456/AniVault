// Custom Cursor
const dot = document.getElementById('cursor-dot');
const ring = document.getElementById('cursor-ring');

if (dot && ring) {
    window.addEventListener('mousemove', (e) => {
        dot.style.left = e.clientX + 'px';
        dot.style.top = e.clientY + 'px';
        ring.style.left = e.clientX + 'px';
        ring.style.top = e.clientY + 'px';
    });

    window.addEventListener('mousedown', () => {
        ring.style.width = '18px';
        ring.style.height = '18px';
    });

    window.addEventListener('mouseup', () => {
        ring.style.width = '28px';
        ring.style.height = '28px';
    });
}