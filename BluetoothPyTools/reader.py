import bluetooth

found_devices = bluetooth.discover_devices(lookup_names=True)

print(f"Found {len(found_devices)} devices.")

for addr, name in found_devices:
    print(f"{addr} - {name}")
