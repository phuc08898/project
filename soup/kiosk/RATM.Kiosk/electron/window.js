import { BrowserWindow, screen } from "electron";
import { format } from "url";
import { join } from "path";
import { configs, windows } from "./config.js";

// import path from 'node:path';
import { fileURLToPath } from 'url';
import { dirname } from 'path';
const __filename = fileURLToPath(import.meta.url);
const __dirname = dirname(__filename);

export const createWindows = () => {
  createWindow(windows.main);
  // createWindow(windows.billboard);
  const displays = screen.getAllDisplays();
  const externalDisplay = displays.find((display) => {
    return display.bounds.x !== 0 || display.bounds.y !== 0;
  });
  if (windows.billboard.visible && externalDisplay) {
    createWindow(windows.billboard);
  }
};

const createWindow = (win) => {
  const displays = screen.getAllDisplays();
  const externalDisplay = displays.find((display) => {
    return display.bounds.x !== 0 || display.bounds.y !== 0;
  });

  const winConfigs = {
    width: 1200,
    height: 700,
    title: win.title,
    webPreferences: {
      nodeIntegration: true,
      backgroundThrottling: false,
      contextIsolation: false,
      webSecurity: false,
      enableRemoteModule: true,
    },
  };

  if (configs.kiosk) {
    winConfigs.fullscreen = true;
    winConfigs.kiosk = true;
    winConfigs.frame = false;
    winConfigs.skipTaskbar = true;
  }

  if (win == windows.billboard && externalDisplay) {
    winConfigs.x = externalDisplay.bounds.x + 50;
    winConfigs.y = externalDisplay.bounds.y + 50;
  }

  win.instance = new BrowserWindow(winConfigs);
  win.instance.setMenuBarVisibility(false); 
  // init as development url
  let winUrl = `http://localhost:${configs.liveServerPort}/${win.hash}`;
  if (process.env.NODE_ENV !== "dev") {
    winUrl = format({
      pathname: join(__dirname, `../dist/stump-kiosk/index.html`),
      protocol: "file:",
      slashes: true,
      hash: win.hash,
    });
  }

  win.instance.loadURL(winUrl);
  

  win.instance.on("page-title-updated", (e) => {
    e.preventDefault();
  });

  if (configs.kiosk) {
    win.instance.maximize();

    win.instance.on("close", (e) => {
      e.preventDefault();
    });

    win.instance.on("closed", function () {
      win.instance = null;
    });

    win.instance.onbeforeunload = (e) => {
      e.returnValue = false;
    };
  }
};
