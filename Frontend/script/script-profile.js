$(document).ready(function() {
    checkAuthProfile();
    becomeAdministratorListener();

    openCreateOrganizationListener();
    createOrganizationListener();

    openCreateEventListener();
    createEventListener();

    openCreateQueueOrganizationListener();
    createQueueOrganizationListener();

    leaveOrganizationListener();

    displayInputAddCardsListener();
});