from Chat import Chat
from Persistent_Data.User_Settings_Global import User_Settings_Global
import time
import asyncio
from async_generator import aclosing

async def main():
    """  This module is used to test out the Chat class and it's add_to_conversation function. This is done directly and without Pythonnet. """

    ## Simulating a user adding their own user provided settings to the user_settings_Global.ini file.
    #User_Settings_Global.set_user_setting('user provided settings', 'OPENAI_ORGANIZATION', 'org-OfawLLUdompASRRKdDXDM6yx')
    #User_Settings_Global.set_user_setting('user provided settings', 'OPENAI_API_KEY', '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')
    #User_Settings_Global.set_user_setting('user provided settings', 'MODEL_ID', 'gpt-3.5-turbo')
    #User_Settings_Global.set_user_setting('user provided settings', 'USER_NAME', 'martin')

    ## Simulating a user changing the persistent settings to the user_settings_Global.ini file.
    #User_Settings_Global.set_user_setting('default settings', 'max_response_tokens', 250)
    #User_Settings_Global.set_user_setting('default settings', 'token_limit', 4096)
    #User_Settings_Global.set_user_setting('default settings', 'temperature', 1)
    #User_Settings_Global.set_user_setting('default settings', 'top_p', 1)
    #User_Settings_Global.set_user_setting('default settings', 'n', 1)
    #User_Settings_Global.set_user_setting('default settings', 'frequency_penalty', 0)
    #User_Settings_Global.set_user_setting('default settings', 'presence_penalty', 0)
    #User_Settings_Global.set_user_setting('default settings', 'stop', None)
    #User_Settings_Global.set_user_setting('default settings', 'logit_bias', None)

    # Call the function to clear the user provided settings
    #User_Settings_Global.clear_user_provided_settings()

    # Load the user settings into the os.environ object.
    # User_Settings_Global().load_User_Settings() # Ikke noedvendig hvis alle vaerdierne saettes i User_Settings_Global.ini ovenover.

    conversation_instance = Chat()

    while True:
        prompt = input('user prompt:')
        print()

        conversation_generator = conversation_instance.add_to_conversation_async(prompt)
        
        async with aclosing(conversation_generator) as gen:
            async for chunk_content, finish_reason, created_local_time, token_cost_latest_message, token_cost_full_conversation in gen:
                print(chunk_content, end='')
            print()
            print()
            
            print("Finish reason:", finish_reason)
            
            print()
            
            # print(created_local_time)
            
            created_local_time = time.localtime(created_local_time)
            
            formatted_time = time.strftime("%d-%m-%Y %H:%M:%S", created_local_time)
            print("Created: " + formatted_time)
            print()

            print(f"Token cost for this request and response: {token_cost_latest_message}")
            print(f"Total tokens used in this conversation so far: {token_cost_full_conversation}")

            print()

# Run the async main function
asyncio.run(main())