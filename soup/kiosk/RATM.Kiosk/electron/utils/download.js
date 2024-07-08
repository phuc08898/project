import { createWriteStream } from "fs";
import request, { head } from "request";

export function downloadAsync(sourceUrl, destinationPath) {
  return new Promise((resolve, reject) => {
    try {
      head(sourceUrl, function (err, res, body) {
        if (err) reject(err);

        request(sourceUrl)
          .pipe(createWriteStream(destinationPath))
          .on("close", resolve);
      });
    } catch (error) {
      reject(error);
    }
  });
}
