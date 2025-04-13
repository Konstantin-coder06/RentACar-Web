document.addEventListener("DOMContentLoaded", function () {
    const modal = document.getElementById("calendarModal");
    const openBtn = document.getElementById("openCalendar");
    const closeBtn = document.querySelector(".close");
    const confirmBtn = document.getElementById("confirmDates");

    const startDateInput = document.getElementById("startDateInput");
    const endDateInput = document.getElementById("endDateInput");
    const form = document.getElementById("dateForm");

    // Parse startDate and endDate (assuming these are defined or passed, e.g., via data attributes or server)
    const start = startDate !== null ? new Date(startDate) : null;
    const end = endDate !== null ? new Date(endDate) : null;

    const isValidStart = start && !isNaN(start.getTime());
    const isValidEnd = end && !isNaN(end.getTime());


    const today = new Date();
    const tomorrow = new Date(today);
    tomorrow.setDate(today.getDate() + 1);

    
    const threeDaysLater = new Date(tomorrow);
    threeDaysLater.setDate(tomorrow.getDate() + 3);

    let defaultDates;
    if (isValidStart && isValidEnd && start <= end && start >= tomorrow) {
        defaultDates = [start, end];
    } else {
        defaultDates = [tomorrow, threeDaysLater];
    }

    const datePicker = flatpickr("#datePicker", {
        mode: "range",
        dateFormat: "Y-m-d",
        minDate: tomorrow, 
        maxDate: new Date().fp_incr(210), 
        inline: true,
        numberOfMonths: 3,
        defaultDate: defaultDates
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

    const searchInput = document.getElementById("searchInput");

    searchInput.addEventListener("input", function () {
        const searchTerm = this.value.toLowerCase();
        const carItems = document.querySelectorAll(".car-item");

        carItems.forEach(item => {
            const title = item.getAttribute("data-title").toLowerCase();
            if (title.includes(searchTerm)) {
                item.style.display = "block";
            } else {
                item.style.display = "none";
            }
        });
    });
});