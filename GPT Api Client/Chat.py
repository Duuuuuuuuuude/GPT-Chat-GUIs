from os import environ as env
import openai
import tiktoken
from Persistent_Data.User_Settings_Global import User_Settings_Global

class Chat:

    def __init__(self):
        self._token_cost_latest_message = 0
        self._token_cost_full_conversation = 0

        self._conversation = []
        #self._conversation.append({"role": "system", "content": "You are a helpfule assistent. Always include a short headline followed by \r\n.", "name": "system"})
        self._conversation.append({"role": "system", "content": "You are a helpfule assistent.", "name": "system"})

        User_Settings_Global().load_User_Settings()

        self.headline = None

        #self.__update_total_num_tokens()
        
    def add_to_conversation(self, prompt):
        """ Time is yielded as a Unix timestamp """

        self._conversation.append({'role': 'user', 'content': prompt, 'name': env["USER_NAME"]}) # Add the user's input to the _conversation.

        response = self.__send_chat_gpt_request()

        # Deletes the oldest message in the _conversation until the _conversation is within the token limit.
        conv_history_tokens = self.__num_tokens_from_messages(env["model_id"])
        while (conv_history_tokens + int(env['MAX_RESPONSE_TOKENS']) >= int(env['TOKEN_LIMIT'])):
            del self._conversation[1] 
            conv_history_tokens = self.num_tokens_from_messages(self._conversation)

        role = None
        full_content = ""
        finish_reason = "" # stop: API returned complete model output
                           # length: Incomplete model output due to max_tokens parameter or token limit
                           # content_filter: Omitted content due to a flag from our content filters
                           # null: API response still in progress or incomplete

        created_local_time = None
        # Yield the response in chunks.
        for i, chunk in enumerate(response):
            if i == 0:
                role = chunk['choices'][0]['delta']['role']
            else:
                content = chunk['choices'][0]['delta'].get('content', '')

                full_content += content

                finish_reason = chunk['choices'][0]['finish_reason']
                created_local_time = chunk['created']

                print(created_local_time)

                yield content, finish_reason, created_local_time, None, None # TODO: Returner en klasse i stedet.

            #if i == 0:
            #    role = chunk['choices'][0]['delta']['role']
            #else:
            #    content = chunk['choices'][0]['delta'].get('content', '')
            #    if self.headline == None:
            #        self.headline = content.split('\r\n')[0] # The first line of the content should be the headline.
            #        yield None, None, None, None, None, self.headline
            #    else:
            #        full_content += content
            #        finish_reason = chunk['choices'][0]['finish_reason']
            #        created_local_time = chunk['created']
            #        yield content, finish_reason, created_local_time, None, None, None

        self._conversation.append({'role': role, 'content': full_content}) # Add the response to the _conversation.

        self.__update_total_num_tokens()

        yield "", finish_reason, created_local_time, self._token_cost_latest_message, self._token_cost_full_conversation # , None

    def __send_chat_gpt_request(self):
        User_Settings_Global.is_required_invironment_variables_set()

        openai.organization = env['OPENAI_ORGANIZATION']
        openai.api_key = env['OPENAI_API_KEY']

        params = {
            "model": env['MODEL_ID'],
            "messages": self._conversation,
            "temperature": int(env['temperature']), # Defaults to 1.
                                                    # What sampling temperature to use, between 0 and 2. 
                                                    # Higher values like 0.8 will make the output more random, 
                                                    # while lower values like 0.2 will make it more focused and deterministic. 
                                                    # We generally recommend altering this or top_p but not both.

            "max_tokens": int(env['MAX_RESPONSE_TOKENS']), # The maximum number of tokens to generate in the chat completion.
                                                           # The total length of input tokens and generated tokens is limited by the model's context length.
            "top_p": int(env['top_p']), # Defaults to 1.
                                        # An alternative to sampling with temperature, 
                                        # called nucleus sampling, 
                                        # where the model considers the results of the tokens with top_p probability mass. 
                                        # So 0.1 means only the tokens comprising the top 10% probability mass are considered. 
                                        # We generally recommend altering this or temperature but not both.

            "n": int(env['n']), # Defaults to 1.
                                # How many chat completion choices to generate for each input message.

            "stream": True, # Defaults to False.
                            # If set, partial message deltas will be sent, like in ChatGPT. 
                            # Tokens will be sent as data-only server-sent events (https://developer.mozilla.org/en-US/docs/Web/API/Server-sent_events/Using_server-sent_events#Event_stream_format) as they become available, 
                            # with the stream terminated by a data: [DONE] message. 
                            # See the OpenAI Cookbook for example code(https://github.com/openai/openai-cookbook/blob/main/examples/How_to_stream_completions.ipynb).

            "frequency_penalty": int(env['frequency_penalty']), # Defaults to 0.
                                                                # Number between -2.0 and 2.0. Positive values penalize new tokens based on their existing frequency in 
                                                                # the text so far, decreasing the model's likelihood to repeat the same line verbatim.
                                                                # See more information about frequency and presence penalties(https://platform.openai.com/docs/api-reference/parameter-details)

            "presence_penalty": int(env['presence_penalty']), # Defaults to 0.
                                                              # Number between -2.0 and 2.0. Positive values penalize new tokens based on whether they appear in the 
                                                              # text so far, increasing the model's likelihood to talk about new topics.
                                                              # See more information about frequency and presence penalties(https://platform.openai.com/docs/api-reference/parameter-details)

            "logit_bias": {} if env['logit_bias'].lower() == 'none' or 'null' or '' else env['logit_bias'] # Defaults to None.
                                                                                                           # Modify the likelihood of specified tokens appearing in the completion.
                                                                                                           # Accepts a json object that maps tokens (specified by their token ID in the tokenizer) to an associated 
                                                                                                           # bias value from -100 to 100. Mathematically, the bias is added to the logits generated by the model prior 
                                                                                                           # to sampling. The exact effect will vary per model, but values between -1 and 1 should decrease or 
                                                                                                           # increase likelihood of selection; values like -100 or 100 should result in a ban or exclusive selection of the 
                                                                                                           # relevant token.
        }

        if env["USER_NAME"].lower() != "none" or "null" or "": # Defaults to nothing.
            params["user"] = env["USER_NAME"]                  # A unique identifier representing your end-user, which can help OpenAI to monitor and detect abuse. 
                                                               # Learn more (https://platform.openai.com/docs/guides/safety-best-practices/end-user-ids).
            
        if env['stop'].lower() != 'none' or "null" or "": # Defaults to ""
            params['stop'] = env['stop']                  # Up to 4 sequences where the API will stop generating further tokens.
            
        response = openai.ChatCompletion.create(**params)

        return response

    def __update_total_num_tokens(self):
        num_tokens_latest_conversation = self.__num_tokens_from_messages(model=env["MODEL_ID"])

        self._token_cost_latest_message = num_tokens_latest_conversation - self._token_cost_full_conversation

        self._token_cost_full_conversation = num_tokens_latest_conversation


    def __num_tokens_from_messages(self, model="gpt-3.5-turbo-0301"): # TODO: Tokens bliver ikke beregnet korrekt.
        """Returns the number of tokens used by a list of messages.
          Currently only works for these models gpt-3.5-turbo-0301, gpt-4-0314 and maybe also gpt-3.5-turbo and gpt-4 as long as they don't change and still use the same encoding as the -0301 and -0314 models.
          If model not found, cl100k_base encoding is assumed.
          https://github.com/openai/openai-cookbook/blob/297c53430cad2d05ba763ab9dca64309cb5091e9/examples/How_to_count_tokens_with_tiktoken.ipynb
        """

        
        try:
            encoding = tiktoken.encoding_for_model(model)
        except KeyError:
            print("Warning: model not found. Using cl100k_base encoding.")
            encoding = tiktoken.get_encoding("cl100k_base")

        ### Warnings: gpt may change over time. Returning num tokens assuming a model.
        if model == "gpt-3.5-turbo":
            print()
            print()
            print("Warning: gpt-3.5-turbo may change over time. Returning num tokens assuming gpt-3.5-turbo-0301.")
            return self.__num_tokens_from_messages(model="gpt-3.5-turbo-0301")
    
        elif model == "gpt-4":
            print()
            print()
            print("Warning: gpt-4 may change over time. Returning num tokens assuming gpt-4-0314.")
            return self.__num_tokens_from_messages(model="gpt-4-0314")
        ###

        elif model == "gpt-3.5-turbo-0301":
            tokens_per_message = 4  # every message follows <|start|>{role/name}\n{content}<|end|>\n
            tokens_per_name = -1  # if there's a name, the role is omitted

        elif model == "gpt-4-0314":
            tokens_per_message = 3
            tokens_per_name = 1 

        else:
            raise NotImplementedError(f"""num_tokens_from_messages() is not implemented for model {model}. See https://github.com/openai/openai-python/blob/main/chatml.md for information on how messages are converted to tokens.""")

        #num_tokens = 0
        #for message in self._conversation:
        #    role_tokens = len(encoding.encode(message.get('role', '')))
        #    content_tokens = len(encoding.encode(message.get('content', '')))
        #    name_tokens = len(encoding.encode(message.get('name', '')))

        #    if message.get('name'):
        #        extra_tokens = 1 if model == "gpt-4-0314" else -1
        #    else:
        #        extra_tokens = 3 if model == "gpt-4-0314" else 4

        #    message_tokens = role_tokens + content_tokens + name_tokens + extra_tokens
        #    num_tokens += message_tokens
        #    print(f"Tokens for the current message: {message} {num_tokens}")

        #num_tokens += 3  # every reply is primed with assistant
        #return num_tokens

        num_tokens = 0
        for message in self._conversation:
            num_tokens += tokens_per_message
            for key, value in message.items():
                if key in ('role', 'content', 'name'): # In case there are other keys in the message dict.
                    num_tokens += len(encoding.encode(value))
                    if key == "name":
                        num_tokens += tokens_per_name

        num_tokens += 2 # every reply is primed with <|start|>assistant<|message|>
        return num_tokens







##############
# Response Json
{
    "choices": [
    {
        "finish_reason": "stop",
        "index": 0,
        "message": {
        "content": "As an AI language model, I am not capable of holding personal beliefs or opinions. The meaning of life is a philosophical question and has different interpretations depending on one's beliefs, culture, and values. Some people believe that the meaning of life is to seek happiness, others believe that it is to fulfill a divine purpose, and others find meaning in personal growth, relationships, and contributing to society.",
        "role": "assistant"
        }
    }
    ],
    "created": 1682071323,
    "id": "chatcmpl-77hsZ9yigsLf8B3CPF3vs8UvAw58w",
    "model": "gpt-3.5-turbo-0301",
    "object": "chat.completion",
    "usage": {
    "completion_tokens": 79, # Completion tokens are the tokens that the API generates and returns.
    "prompt_tokens": 26, # Prompt tokens are the tokens that you provide to the API.
    "total_tokens": 105 # Total tokens are the total number of tokens that the API has consumed in this request.
    }
}


    
# Response Json when stream == True.
{
  "choices": [
    {
      "delta": {
        "role": "assistant"
      },
    #   "finish_reason": null,
      "index": 0
    }
  ],
  "created": 1677825464,
  "id": "chatcmpl-6ptKyqKOGXZT6iQnqiXAH8adNLUzD",
  "model": "gpt-3.5-turbo-0301",
  "object": "chat.completion.chunk"
}
{
  "choices": [
    {
      "delta": {
        "content": "\n\n"
      },
    #   "finish_reason": null,
      "index": 0
    }
  ],
  "created": 1677825464,
  "id": "chatcmpl-6ptKyqKOGXZT6iQnqiXAH8adNLUzD",
  "model": "gpt-3.5-turbo-0301",
  "object": "chat.completion.chunk"
}
{
  "choices": [
    {
      "delta": {
        "content": "2"
      },
    #   "finish_reason": null,
      "index": 0
    }
  ],
  "created": 1677825464,
  "id": "chatcmpl-6ptKyqKOGXZT6iQnqiXAH8adNLUzD",
  "model": "gpt-3.5-turbo-0301",
  "object": "chat.completion.chunk"
}
{
  "choices": [
    {
      "delta": {},
      "finish_reason": "stop",
      "index": 0
    }
  ],
  "created": 1677825464,
  "id": "chatcmpl-6ptKyqKOGXZT6iQnqiXAH8adNLUzD",
  "model": "gpt-3.5-turbo-0301",
  "object": "chat.completion.chunk"
}

#Every response will include a finish_reason. The possible values for finish_reason are:

#    stop: API returned complete model output
#    length: Incomplete model output due to max_tokens parameter or token limit
#    content_filter: Omitted content due to a flag from our content filters
#    null: API response still in progress or incomplete