export const downloadFile = (fileBlob: any, name: string) => {
  const url = window.URL.createObjectURL(
    new Blob([fileBlob.data], {
      type: fileBlob.headers['content-type'],
    })
  );

  const link = document.createElement('a');
  link.href = url;
  link.setAttribute('download', name);
  document.body.appendChild(link);
  link.click();
  link.remove();
  link.style.display = 'none';
  window.URL.revokeObjectURL(url);
};
