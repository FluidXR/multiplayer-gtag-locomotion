(function() {
    // Function to check for the element
    function checkForElement() {
        var element = document.querySelector('.ytp-unmute.ytp-popup.ytp-button.ytp-unmute-animated');
        if (element) {
            // Check if the element is visible
            var isVisible = window.getComputedStyle(element).display !== 'none';
            if (isVisible) {
                if (window.vuplex) {
                    // The window.vuplex object already exists, so go ahead and send the message.
                    window.vuplex.postMessage({ type: 'needUnmute' });
                } else {
                    // The window.vuplex object hasn't been initialized yet because the page is still
                    // loading, so add an event listener to send the message once it's initialized.
                    window.addEventListener('vuplexready', () => {
                        window.vuplex.postMessage({ type: 'needUnmute' });
                    });
                }
                
            }
        }
    }

    // Inject this script into the page
    document.addEventListener('DOMContentLoaded', function() {
        checkForElement();
    });

    // You can also set an interval to continuously check for the element
    var intervalId = setInterval(checkForElement, 1000); // Check every 1 second

    // Optional: Stop checking after a certain amount of time
    setTimeout(function() {
        clearInterval(intervalId);
        console.log('Stopped checking for the element.');
    }, 10000); // Stop after 10 seconds
})();

console.log('ytshorts.js loaded');