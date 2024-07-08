import { app, BrowserWindow } from "electron";
import { join } from "path";
import { existsSync, mkdirSync } from "fs";
import { register } from "./command.js";
import { createWindows } from "./window.js"; 

const appdataExist = existsSync(join(process.cwd(), "appdata"));
if (!appdataExist) {
  mkdirSync(join(process.cwd(), "appdata/resources"), {
    recursive: true,
  });
}

register();

app.whenReady().then(() => {
  createWindows();

  app.on("activate", () => {
    if (BrowserWindow.getAllWindows().length === 0) {
      createWindows();
    }
  });
});

app.on("window-all-closed", () => {
  if (process.platform !== "darwin") {
    app.quit();
  }
});