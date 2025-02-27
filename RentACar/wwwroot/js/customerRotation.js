const dashboardBtn = document.querySelector("#dashboard_btn");
const customerBtn = document.querySelector("#cutomer_btn");
const tableTitle = document.querySelector("#table_title");
const tableHeaders = document.querySelector("#table_headers");
const tableBody = document.querySelector("#table_body");

customerBtn.addEventListener("click", () => {
    dashboardBtn.classList.remove("active");
    customerBtn.classList.add("active");

    // Промяна на заглавието
    tableTitle.textContent = "Customers List";

    // Смяна на хедърите
    tableHeaders.innerHTML = `
                <th>Name</th>
                <th>Email</th>
                <th>Phone</th>
                <th>Status</th>
            `;

    // Смяна на съдържанието
    tableBody.innerHTML = `
        @for (int i = 0; i < Model.Customers.Count; i++)
        {
                        <tr>
                            <td>@Model.Customers[i].Name</td>
                           
                            <td class="active">Active</td>
                        </tr>
        }
            `;
});

dashboardBtn.addEventListener("click", () => {
    customerBtn.classList.remove("active");
    dashboardBtn.classList.add("active");

    // Възстановяване на заглавието
    tableTitle.textContent = "Pending Cars";

    // Възстановяване на хедърите
    tableHeaders.innerHTML = `
                <th>Car Brand</th>
                <th>Model</th>
                <th>Created At</th>
                <th>Status</th>
            `;

    // Възстановяване на съдържанието
    tableBody.innerHTML = `
        @for (int i = 0; i < Model.Pending.Count; i++)
        {
                        <tr>
                            <td>@Model.Pending[i].Brand</td>
                            <td>@Model.Pending[i].Model</td>
                            <td>@Model.Pending[i].CreatedAt</td>
                            <td class="warning">Pending</td>
                            <td>
                                <div class="button">
                                    <a asp-action="EditPendingCar" asp-controller="Car" asp-route-id="@Model.Pending[i].Id" class="primary">Details</a>
                                </div>
                            </td>
                        </tr>
        }
            `;
});