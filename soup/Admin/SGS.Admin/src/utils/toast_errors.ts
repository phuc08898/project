import { toast } from 'react-toastify';

export const showToastErrors = (errors: any) => {
  for (let key in errors) {
    if (errors.hasOwnProperty(key)) {
      // console.log(key, errors.hasOwnProperty(key));
      key === "message" && toast.error(errors[key]);
    }
  }
};
