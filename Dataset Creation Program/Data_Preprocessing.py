import pandas as pd
import os
import glob
import re

def clean_text(text):
    """Cleans text by removing extra whitespace and special characters."""
    text = re.sub(r'\s+', ' ', text).strip()
    text = re.sub(r'[^\w\s,.-]', '', text)
    return text

def create_csv_dataset(data_dir, output_file="Data\AIFC_Dataset.csv"):
    """Creates a CSV dataset with each line from input files as a separate row."""

    data = []

    file_paths = glob.glob(os.path.join(data_dir, "*"))

    for file_path in file_paths:
        try:
            filename = os.path.basename(file_path)
            # Correct logic for label and header assignment
            if "inventoryreport" in filename:
                label = "InventoryReport"
                header_text = "Inventory Report"
            elif "invoice" in filename or "invoices" in filename:
                label = "Invoice"
                header_text = "Invoice"
            elif "orders" in filename or "Order" in filename or "PO" in filename or "PurchaseOrder" in filename:
                label = "PurchaseOrder"
                header_text = "Purchase Order"
            elif "receipt" in filename:
                label = "Receipt"
                header_text = "Receipt"
            elif "shipping" in filename or "ShippingDocuments" in filename:
                label = "ShippingDocument"
                header_text = "Shipping Manifest"
            else:
                continue  # Skip files that don't match any known type

            if label is None: #check if the label is none
                continue

            try:
                with open(file_path, 'r', encoding='utf-8') as f:
                    for line in f:
                        cleaned_line = clean_text(line.strip())
                        if cleaned_line:
                            text = f"{header_text},\"{cleaned_line}\""
                            data.append({"Text": text, "Label": label})

            except UnicodeDecodeError:
                with open(file_path, 'r', encoding='latin-1') as f:
                    for line in f:
                        cleaned_line = clean_text(line.strip())
                        if cleaned_line:
                            text = f"{header_text},\"{cleaned_line}\""
                            data.append({"Text": text, "Label": label})
            except Exception as e:
                print(f"Error reading file {file_path}: {e}")

        except Exception as e:
            print(f"Error processing file {file_path}: {e}")

    df = pd.DataFrame(data)
    df.to_csv(output_file, index=False, encoding='utf-8')  # Explicitly set encoding for CSV
    print(f"CSV file '{output_file}' created successfully.")

# Example usage:
data_directory = "Data\StructuredData"
create_csv_dataset(data_directory)