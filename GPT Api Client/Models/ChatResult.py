from dataclasses import dataclass
from datetime import datetime
from typing import Optional

@dataclass
class ChatResult:
    content_chunk: str = ""
    finish_reason: str = "" # stop: API returned complete model output
                            # length: Incomplete model output due to max_tokens parameter or token limit
                            # content_filter: Omitted content due to a flag from our content filters
                            # null: API response still in progress or incomplete
    created_local_date_time: Optional[datetime] = None
    token_cost_latest_message: Optional[int] = None
    token_cost_full_conversation: Optional[int] = None
