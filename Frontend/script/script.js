$(document).ready(function() {
    changeLoginRegistarionListener();
    
    registrationSubmitListener();
    loginSubmitListener();
    logoutSubmitListener();

    checkAuthIndex(0);

    getQueues();
    //queueListDisplayListener();
});