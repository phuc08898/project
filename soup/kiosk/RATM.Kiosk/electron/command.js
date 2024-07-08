import { ipcMain } from "electron";
import path from "path";
import { windows } from "./config.js";
import { exec } from "child_process";
import { DatabaseContext } from "./database/prisma.js"
import { Prisma } from '@prisma/client'

export const register = () => {
  registerDatabaseCommand();
  registerFileCommands();
  registerPrintCommands();

  ipcMain.on("billboard.send", (event, data) => {
    try {
      windows.billboard.instance.webContents.send("billboard.event", data);
    } catch { }
  });

  ipcMain.on("system.cmd", (event, cmd) => {
    try {
      exec(cmd, (error, stdout, stderr) => {
        if (error) {
          event.returnValue = {
            error: true,
            data: error.message,
          };
          return;
        }
        if (stderr) {
          event.returnValue = {
            error: true,
            data: stderr,
          };
          return;
        }
        event.returnValue = {
          error: false,
          data: stdout,
        };
      });
    } catch {
      event.returnValue = {
        error: true,
      };
    }
  });

  ipcMain.on("system.isRunning", (event, processName) => {
    try {
      const exec = require("child_process").exec;

      const isRunning = (query, cb) => {
        let platform = process.platform;
        let cmd = "";
        switch (platform) {
          case "win32":
            cmd = `tasklist`;
            break;
          case "darwin":
            cmd = `ps -ax | grep ${query}`;
            break;
          case "linux":
            cmd = `ps -A`;
            break;
          default:
            break;
        }
        exec(cmd, (err, stdout, stderr) => {
          cb(stdout.toLowerCase().indexOf(query.toLowerCase()) > -1);
        });
      };

      isRunning(processName, (status) => {
        event.returnValue = {
          error: false,
          data: status,
        };
      });
    } catch {
      event.returnValue = {
        error: true,
      };
    }
  });

  ipcMain.on("system.platform", (event) => {
    try {
      let platform = process.platform;
      event.returnValue = {
        error: false,
        data: platform,
      };
    } catch {
      event.returnValue = {
        error: true,
      };
    }
  });

  ipcMain.on("system.run", (event, cmd) => {
    try {
      exec(cmd, (error, stdout, stderr) => {
        if (error) {
          event.returnValue = {
            error: true,
            data: error.message,
          };
          return;
        }
        if (stderr) {
          event.returnValue = {
            error: true,
            data: stderr,
          };
          return;
        }
        event.returnValue = {
          error: false,
          data: stdout,
        };
      });

      event.returnValue = {
        error: false,
        data: null,
      };
    } catch {
      event.returnValue = {
        error: true,
      };
    }
  });
};

function registerDatabaseCommand() {
  ipcMain.handle("database.queryAsync", async (event, cmd) => {
    try {
      const prisma = (new DatabaseContext()).GetInstance();
      const response = await prisma.$queryRaw(Prisma.raw(`${cmd}`));
      prisma.$disconnect();
      return response;
    }
    catch (error) {
      return { error: error }
    }
  });

  ipcMain.handle("database.executeAsync", async (event, cmd) => {
    try {
      const prisma = (new DatabaseContext()).GetInstance();
      const affectedRows = await prisma.$executeRaw(Prisma.raw(`${cmd}`));
      prisma.$disconnect();
      return affectedRows;
    }
    catch (error) {
      return { error: error }
    }
  });
}

function registerFileCommands() {
  ipcMain.handle("file.copyAsync", async (event, args) => {
    try {
      await require("./utils/file").copyAsync(args.from, args.destFileName);

      return {};
    } catch (error) {
      return { error: error };
    }
  });

  ipcMain.handle("file.removeAsync", async (event, args) => {
    try {
      await require("./utils/file").removeAsync(args.fileName);

      return {};
    } catch (error) {
      return { error: error };
    }
  });

  ipcMain.handle("file.getResourceDirAsync", async (event, args) => {
    try {
      const result = await require("./utils/file").getResourceDirAsync();

      return { data: result };
    } catch (error) {
      return { error: error };
    }
  });

  ipcMain.handle("file.selectFolderAsync", async (event, args) => {
    try {
      const result = await require("./utils/file").selectFolderAsync();

      return { data: result };
    } catch (error) {
      return { error: error };
    }
  });

  ipcMain.handle("file.downloadAsync", async (event, args) => {
    try {
      const dest = path.join(
        process.cwd(),
        "appdata",
        "resources",
        args.fileName
      );

      await require("./utils/download").downloadAsync(args.url, dest);

      return {};
    } catch (error) {
      return { error: error };
    }
  });
}

function registerPrintCommands() {
  ipcMain.on("print", async (event, args) => {
    try {
      await require("./utils/print").silentPrintAsync(
        args.options,
        args.template
      );
      event.returnValue = {
        error: false,
      };
    } catch (ex) {
      event.returnValue = {
        error: true,
        exception: ex,
      };
    }
  });
}