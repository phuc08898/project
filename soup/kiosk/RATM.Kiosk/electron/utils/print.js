import { BrowserWindow } from "electron";

export function silentPrintAsync(options, template) {
  return new Promise((onDone, onError) => {
    try {
      let printWindow = new BrowserWindow({
        show: false,
        webPreferences: {
          nodeIntegration: true,
          backgroundThrottling: false,
          contextIsolation: false,
          webSecurity: false,
          enableRemoteModule: true,
        },
      });

      printWindow.once("ready-to-show", () => printWindow.hide());
      printWindow.loadURL(`data:text/html;charset=utf-8,${template}`);
      printWindow.webContents.on("did-finish-load", () => {
        printWindow.webContents.print(options, (success, reason) => {
          printWindow.close();
          onDone();
        });
      });

      printWindow.on("closed", function () {
        printWindow = null;
      });
    } catch (error) {
      onError(error);
    }
  });
}
