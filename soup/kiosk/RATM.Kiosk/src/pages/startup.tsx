import { IConfigDto } from "@/interfaces"; 
import { ConfigService } from "@/core/services";
import { useEffect, useRef, useState } from "react";

import { useForm } from 'react-hook-form';
import { toast } from "react-toastify";

export const Startup = () => {
 
  const [configs, setConfigs]= useState<IConfigDto[]>([]);
  const configService = new ConfigService();
  const [isEdit, setIsEdit] = useState<boolean>(false);

  const {
    handleSubmit,
    register,
    setValue,
    reset,
    formState: { errors },
  } = useForm<IConfigDto>();

  useEffect(() => {
    loadData();
  }, []);
  
  
  const onSubmitFormConfig = async (data: IConfigDto) =>{
    configService.InsertAsync([data]).then((resp) => {
      if(resp.error) {
        toast.error(resp.error.message);        
        return;
      }
      toast.success(`affected rows ${resp}`);
      reset();
      loadData();
    })
  }
  const onSubmitFormConfigEdit = async (data: IConfigDto) =>{
    configService.UpdateAsync(data.key, {
      value: data.value,
      desc: data.desc
    }).then((resp) => {
      if(resp.error) {
        toast.error(resp.error.message);        
        return;
      }
      toast.success(`updated rows ${resp}`);
      reset();
      loadData();
    })
  }
  const onDelete = async (key: string) => {
    configService.DeleteAsync(key).then((resp) => {
      if(resp.error) {
        toast.error(resp.error.message);        
        return;
      }
      toast.success(`affected rows ${resp}`);
     
      loadData();
    });

  }
  const onDetail = (data: IConfigDto) => {
    setValue("key", data.key);
    setValue("value", data.value);
    setValue("desc", data.desc);
    
    setIsEdit(true);
  }
  
  const clearForm = () => { 
    reset();
    setIsEdit(false);

  }
  const loadData = () => {
    configService.GetAllAsync().then((res) => {
      if (!res.error) {
        setConfigs(res);       
        return;
      }
      toast.error(res.error.message);
    })
  }
  return (
    <>
      <div className="relative overflow-x-auto">
        <table className="w-full text-sm text-left rtl:text-right text-gray-500 dark:text-gray-400">
          <thead className="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
            <tr>
              <th scope="col" className="px-6 py-3">
                Key
              </th>
              <th scope="col" className="px-6 py-3">
                Value 
              </th>
              <th scope="col" className="px-6 py-3">
                Desc
              </th>
              <th  className="px-6 py-3">
              
              </th>
             
            </tr>
          </thead>
          <tbody>
            {
                configs.map((config: IConfigDto, index: number) => {
                    return (
                        <tr key={`${config.key}-${index}`} className="bg-white border-b dark:bg-gray-800 dark:border-gray-700">
                        <th
                          scope="row"
                          className="px-6 py-4 font-medium text-gray-900 whitespace-nowrap dark:text-white"
                        >
                            {config.key}    
                        </th>
                        <td className="px-6 py-4">{config.value}</td>
                        <td className="px-6 py-4">{config.desc}</td>
                        <td>
                          <button onClick={() => onDelete(config.key)}>Delete</button>
                          <button onClick={() => onDetail(config)}>Detail</button>
                        </td> 
                      </tr>
                    )
                })
            }
          </tbody>
        </table>
        <form 
            onSubmit={!isEdit ? handleSubmit(onSubmitFormConfig): handleSubmit(onSubmitFormConfigEdit)}>
            <input id={'key'}
              {...register('key')}
              type="text"
              placeholder={'Nhập key'}
              className="w-full input input-bordered"/>
            <input id={'value'}
              {...register('value')}
              type="text"
              placeholder={'Nhập value'}
              className="w-full input input-bordered"/>
            <input id={'desc'}
              {...register('desc')}
              type="text"
              placeholder={'Nhập desc'}
              className="w-full input input-bordered"/>
              
            <button className="bg-red-700 text-xl font-bold" type={'submit'}>Submit</button>
            <button onClick={clearForm}>Clear</button>
        </form>

        
      </div>
    </>
  );
};
