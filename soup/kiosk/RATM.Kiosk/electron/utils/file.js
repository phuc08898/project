import { dialog } from "electron";
import { remove, copy } from "fs-jetpack";
import { join } from "path";

async function selectFolderAsync() {
  return await dialog.showOpenDialog({
    properties: ["openDirectory"],
  });
}

function removeAsync(fileName) {
  return new Promise((ok, err) => {
    try {
      remove(
        join(process.cwd(), "appdata", "resources", fileName)
      );
      ok();
    } catch (error) {
      err(err);
    }
  });
}

function getResourceDirAsync() {
  return new Promise((ok, err) => {
    try {
      ok(join(process.cwd(), "appdata", "resources"));
    } catch (error) {
      err(err);
    }
  });
}

function copyAsync(from, destFileName) {
  return new Promise((ok, err) => {
    try {
      copy(
        from,
        join(process.cwd(), "appdata", "resources", destFileName)
      );
      ok();
    } catch (error) {
      err(err);
    }
  });
}

const _copyAsync = copyAsync;
export { _copyAsync as copyAsync };
const _getResourceDirAsync = getResourceDirAsync;
export { _getResourceDirAsync as getResourceDirAsync };
const _removeAsync = removeAsync;
export { _removeAsync as removeAsync };
const _selectFolderAsync = selectFolderAsync;
export { _selectFolderAsync as selectFolderAsync };
