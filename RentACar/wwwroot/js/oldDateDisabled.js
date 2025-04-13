document.addEventListener('DOMContentLoaded', function () {
    const startDateInput = document.getElementById('StartDay');
    const endDateInput = document.getElementById('EndDay');

    const today = new Date();
    const tomorrow = new Date(today);
    tomorrow.setDate(today.getDate() + 1);
    const tomorrowFormatted = tomorrow.toISOString().split('T')[0];

    startDateInput.min = tomorrowFormatted;
    endDateInput.min = tomorrowFormatted;

    startDateInput.addEventListener('change', function () {
        if (startDateInput.value) {
            endDateInput.min = startDateInput.value;

            if (endDateInput.value && endDateInput.value < startDateInput.value) {
                endDateInput.value = startDateInput.value;
            }
        }
    });
});