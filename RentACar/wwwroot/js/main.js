const togglePasswordBtn = document.getElementById("toggle-password");

togglePasswordBtn.addEventListener("click", () => {
    const passwordInput = document.getElementById("password");
    const icon = togglePasswordBtn.querySelector("i");
    const isVisible = passwordInput.type === "text";
    passwordInput.type = isVisible ? "password" : "text";
    icon.className = isVisible ? "ri-eye-off-line" : "ri-eye-line";
});
