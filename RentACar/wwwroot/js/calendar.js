document.addEventListener("DOMContentLoaded", function () {
    const modal = document.getElementById("calendarModal");
    const openBtn = document.getElementById("openCalendar");
    const closeBtn = document.querySelector(".close");
    const confirmBtn = document.getElementById("confirmDates");

    const startDateInput = document.getElementById("startDateInput");
    const endDateInput = document.getElementById("endDateInput");
    const form = document.getElementById("dateForm");

    const start = startDate !== null ? new Date(startDate) : null;
    const end = endDate !== null ? new Date(endDate) : null;

    const isValidStart = start && !isNaN(start.getTime());
    const isValidEnd = end && !isNaN(end.getTime());

    const today = new Date();
    const threeDaysLater = new Date(today);
    threeDaysLater.setDate(today.getDate() + 3);
    if (openBtn) {
        openBtn.addEventListener("click", function () {
            modal.style.display = "block";
            console.log("Modal opened");
        });
    }
    let defaultDates;
    if (isValidStart && isValidEnd && start <= end) {
        defaultDates = [start, end];
    } else {
        defaultDates = [today, threeDaysLater];
    }

    const datePicker = flatpickr("#datePicker", {
        mode: "range",
        dateFormat: "Y-m-d",
        minDate: "today",
        maxDate: new Date().fp_incr(210),
        inline: true,
        numberOfMonths: 3,
        defaultDate: defaultDates
    });



    closeBtn.addEventListener("click", function () {
        modal.style.display = "none";
    });






    confirmBtn.addEventListener("click", function () {
        let selectedDates = datePicker.selectedDates;
        console.log("Selected Dates:", selectedDates);
        if (selectedDates && selectedDates.length === 2) {
            let start = selectedDates[0].toISOString().split("T")[0];
            let end = selectedDates[1].toISOString().split("T")[0];
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