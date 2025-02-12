mergeInto(LibraryManager.library, {
    showAlert: function (message) {
        alert(Pointer_stringify(message));  // 문자열 포인터를 JS 문자열로 변환
    }
});
