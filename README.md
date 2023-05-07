# Still under construction.

To Run this:
   1) 'GPT-Chat-GUIs/GPT' Api Client/requirements.txt has to be installed in the 'GPT Api Client' project.
   2) An .env file has to be present in the 'GPT-Chat-GUIs/GPT Api Client/Persistent_Data/' directory, with the following variables set:
       OPENAI_ORGANIZATION= ;Found here https://platform.openai.com/account/org-settings
       OPENAI_API_KEY= ;Found here: https://platform.openai.com/account/api-keys
       MODEL_ID= ;E.g. 'gpt-3.5-turbo-0301'
       
Things to be aware of, for now:
   1) 'GPT Api Client' will not be copied into the Middleware project if it has not been build manually before running the program. As it does not build automatically when           startet.
   
   ~~2) The path to the root directory cannot be very long, and there is no warning telling you what the problem is, if that is the case.~~
   
   3) This software is still in the very early days of development. Expect a few bugs, missing features and a very ugly GUI.
   
   ~~4) It is still running singlethreaded (For now), so GUI will freeze when it should print things out in chunks as they are returned from the Api.~~
 
 Iteration 4
 ![Skærmbillede 2023-05-06 183614](https://user-images.githubusercontent.com/85315025/236636486-f8b567d2-6f50-4378-ae93-44542b718cbe.png)

 
 Iteration 3
 ![Skærmbillede 2023-05-05 235818](https://user-images.githubusercontent.com/85315025/236575488-79ef2643-e2ba-4b41-b2f6-b0502aea83e6.png)

 
Iteration 2
![Skærmbillede 2023-05-04 124308](https://user-images.githubusercontent.com/85315025/236182140-aeb73643-a30d-4c21-a3f4-f4f5cd05e3d3.png)

Iteration 1
![Skærmbillede 2023-04-30 235605](https://user-images.githubusercontent.com/85315025/235378010-5202531f-8d0e-4263-aeae-f42fe33aa95f.png)
