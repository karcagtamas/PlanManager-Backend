function manageDownload(name, contentType, content) {
    const nameVal = BINDING.conv_string(name);
    const contentTypeVal = BINDING.conv_string(contentType);
    const contentVal = Blazor.platform.toUint8Array(content);

    const file = new File([contentVal], nameVal, { type: contentTypeVal });
    const exportUrl = URL.createObjectURL(file);

    const a = document.createElement("a");
    document.body.appendChild(a);
    a.href = exportUrl;
    a.download = nameVal;
    a.target = "_self";
    a.click();

    URL.revokeObjectURL(exportUrl);
}