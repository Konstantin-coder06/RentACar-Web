document.addEventListener('DOMContentLoaded', function () {
    const startDateInput = document.getElementById('StartDay');
    const endDateInput = document.getElementById('EndDay');

    const today = new Date().toISOString().split('T')[0];
    startDateInput.min = today;
    endDateInput.min = today;

    startDateInput.addEventListener('change', function () {
        if (startDateInput.value) {
            endDateInput.min = startDateInput.value;

            if (endDateInput.value && endDateInput.value < startDateInput.value) {
                endDateInput.value = startDateInput.value;
            }
        }
    });
});