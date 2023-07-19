import os
from dotenv import load_dotenv
import configparser
from os import environ as env


class User_Settings_Global:
    """This class is used to load the user settings from the Persistent_Data/User_Settings_Global.ini file and update the os.environ object."""

    required_env_vars = [
        "OPENAI_ORGANIZATION",
        "OPENAI_API_KEY",
        "MODEL_ID",
        "max_response_tokens",
        "token_limit",
        "temperature",
        "top_p",
        "n",
        "frequency_penalty",
        "presence_penalty",
        "stop",
        "logit_bias",
    ]

    INI_FILE_PATH = os.path.join(os.path.dirname(os.path.abspath(__file__)), "User_Settings_Global.ini")  # Has to be in the same folder as the User_Settings_GLobal.ini file
    ENV_FILE_PATH = os.path.join(os.path.dirname(os.path.abspath(__file__)), ".env")

    @staticmethod
    def load_User_Settings():
        """Update the os.environ object with the value from the Persistent_User_Data/User_Settings_Global.ini file if present and not set to None, null or empty, otherwise use the value from the .env file."""
        # Load .env variables
        load_dotenv(User_Settings_Global.ENV_FILE_PATH)

        # Loads user settings from the Persistent_Data/user_settings.ini file.
        user_config = configparser.ConfigParser()
        user_config.read(User_Settings_Global.INI_FILE_PATH)

        for key in user_config["user settings"]:

            #value = user_config.get("user settings", key).lower()
            #is_path = os.path.isfile(User_Settings_Global.ENV_FILE_PATH)

            if user_config.get("user settings", key).lower() not in ["none", "null", ""]:
                # Update the os.environ object with the user value from the Persistent_Data/User_Settings_Global.ini file if present.
                os.environ[key] = user_config.get("user settings", key)
            else:
                if not os.path.isfile(User_Settings_Global.ENV_FILE_PATH): 
                    os.environ[key] = "None"

        for key in user_config["default settings"]:
            if user_config.get("default settings", key).lower() not in ["none", "null", ""]:
                # Update the os.environ object with the default value from the Persistent_Data/User_Settings_Global.ini file.
                os.environ[key] = user_config.get("default settings", key)
            else:
                os.environ[key] = "None"

    @staticmethod
    def is_required_invironment_variables_set():
        """Checks if the environment variable is set and is not 'none', 'null', '' or even exists at all."""
        for required_Var_key in User_Settings_Global.required_env_vars:
            if (
                required_Var_key not in os.environ
                or os.environ[required_Var_key] == "none"
                or os.environ[required_Var_key] == ""
                or os.environ[required_Var_key] == "null"
            ):  # Would be in debug mode or the user hasn't set all the variables here.
                if (
                    required_Var_key not in os.environ
                ):  # The environment variable does not exists.
                    raise EnvironmentError(
                        f"The required environment variable '{required_Var_key}' was not found. Make sure the variable has been set in the User_Settings_Global.ini file if running in relase mode, or the .env file if running in debug mode."
                    )
                else:  # The variable exists but is set to 'none', 'null', or ''.
                    raise EnvironmentError(
                        f"The required environment variable '{required_Var_key}' is required to be set int the User_Settings_Global.ini file if in release mode, or the .env file if in debug mode, but was not set. '{required_Var_key}' was set to '{os.environ[required_Var_key]}'"
                    )

    @staticmethod
    def set_user_setting(section, key, value):
        """Add or update a variable in the Persistent_Data/User_Settings_Global.ini file and update the os.environ object."""
        config = configparser.ConfigParser()
        config.read(User_Settings_Global.INI_FILE_PATH)

        if section not in config:
            config[section] = {}

        config[section][key] = value
        with open(User_Settings_Global.INI_FILE_PATH, "w") as configfile:
            config.write(configfile)

        # Update the os.environ object with the new value
        os.environ[key] = value

    @staticmethod
    def get_user_settings(key):
        return env[key]
        
    # @staticmethod
    # def clear_user_settings():
    #    """ Clear the Persistent_Data/User_Settings_Global.ini file except for the sections. The default settings and comments are kept. """

    #    config = configparser.ConfigParser()
    #    config.read(User_Settings_Global.INI_FILE_PATH)

    #    # Create a new config with only section headers
    #    new_config = configparser.RawConfigParser()
    #    for section in config.sections():
    #        new_config.add_section(section)
    #        if section == 'default settings':
    #            for key, value in config.items(section):
    #                new_config.set(section, key, value)

    #    # Write the new config to the file
    #    with open(User_Settings_Global.INI_FILE_PATH, 'w') as configfile:
    #        new_config.write(configfile)
