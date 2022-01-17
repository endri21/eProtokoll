async

function triggerFileDownload(fileName, url) {
    const anchorElement = document.createElement('a');
    anchorElement.href = url;

    if (fileName) {
        anchorElement.download = fileName;
    }

    anchorElement.click();
    anchorElement.remove();
}

(function (window) {
    window.download =
        window.download || function (fileName, contentStreamReference) {

            const arrayBuffer = await contentStreamReference.arrayBuffer();
            const blob = new Blob([arrayBuffer]);

            const url = URL.createObjectURL(blob);

            triggerFileDownload(fileName, url);

            URL.revokeObjectURL(url);
        }
    window.show =
        window.show || function (id) {
            $(`#${id}`).show()
        }
    window.hide =
        window.hide || function (id) {
            $(`#${id}`).hide()
        }
})(window);




