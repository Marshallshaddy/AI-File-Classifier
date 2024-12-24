import pandas as pd
import numpy as np

# Load the dataset
file_path = "Data\AIFC_Dataset.csv"
df = pd.read_csv(file_path)

# Shuffle the dataset
shuffled_df = df.sample(frac=1, random_state=None)

# Save the shuffled dataset to a new CSV file
shuffled_file_path = "Data\Shuffled_AIFC_Dataset.csv"
shuffled_df.to_csv(shuffled_file_path, index=False)

print('Shuffled dataset created and saved to:', shuffled_file_path)