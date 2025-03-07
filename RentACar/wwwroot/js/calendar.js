document.addEventListener("DOMContentLoaded", function () {
    const modal = document.getElementById("calendarModal");
    const openBtn = document.getElementById("openCalendar");
    const closeBtn = document.querySelector(".close");
    const confirmBtn = document.getElementById("confirmDates");

    const startDateInput = document.getElementById("startDateInput");
    const endDateInput = document.getElementById("endDateInput");
    const form = document.getElementById("dateForm");


    var startDate = '@(Model.StartDate?.ToString("yyyy-MM-dd") ?? "")';
    var endDate = '@(Model.EndDate?.ToString("yyyy-MM-dd") ?? "")';

    const datePicker = flatpickr("#datePicker", {
        mode: "range",
        dateFormat: "Y-m-d",
        minDate: "today",
        maxDate: new Date().fp_incr(210),
        inline: true,
        numberOfMonths: 3,
        defaultDate: startDate && endDate ? [startDate, endDate] : []
    });


    openBtn.addEventListener("click", function () {
        modal.style.display = "flex";
    });


    closeBtn.addEventListener("click", function () {
        modal.style.display = "none";
    });

    window.addEventListener("click", function (event) {
        if (event.target === modal) {
            modal.style.display = "none";
        }
    });


    confirmBtn.addEventListener("click", function () {
        let selectedDates = datePicker.selectedDates;
        if (selectedDates.length === 2) {
            // Use 'en-CA' locale to get YYYY-MM-DD format in local time
            let start = selectedDates[0].toLocaleDateString('en-CA');
            let end = selectedDates[1].toLocaleDateString('en-CA');

            startDateInput.value = start;
            endDateInput.value = end;

            modal.style.display = "none";
            form.submit();
        } else {
            alert("Please select both start and end dates.");
        }
    });
});