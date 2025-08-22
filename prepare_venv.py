import io
import json
import pathlib
import subprocess
import sys
import urllib.request
import venv
import zipfile


COILSNAKE_REV = "d806fa6"
SNES9X_VER = "1.63"
ONE_MiB = 2 ** 20

here = pathlib.Path(__file__).parent

rom = here / "EarthBound.smc"
if not rom.is_file():
    print(f"EarthBound ROM not found. Please place EarthBound.smc in {here} before running this script.", file=sys.stderr)
    exit(-1)

shrinefox_io = here.parent / "ShrineFox.IO"
if not shrinefox_io.is_dir():
    print("Cloning ShrineFox.IO...")
    subprocess.run([
        "git", "clone", "https://github.com/ShrineFox/ShrineFOX.IO"
    ], check=True, cwd=here.parent)

venv_path = here / "venv"
python_exe = venv_path / "Scripts" / "python.exe"

if not python_exe.is_file():
    print("Creating venv...")
    builder = venv.EnvBuilder(with_pip=True)
    builder.create(venv_path)

coilsnake_cli_exe = venv_path / "Scripts" / "coilsnake-cli.exe"
if not coilsnake_cli_exe.is_file():
    print(f"Installing CoilSnake @{COILSNAKE_REV} in Venv (make sure Visual C++ is installed)...")
    subprocess.run([
        python_exe, "-m", "pip", "install", f"https://github.com/pk-hack/CoilSnake/tarball/{COILSNAKE_REV}"
    ], check=True)


if rom.stat().st_size < 6 * ONE_MiB:
    print("Expanding ROM with Coilsnake...")
    subprocess.run([
        coilsnake_cli_exe, "expand", rom, "true"
    ], check=True)

snes9x_exe = venv_path / "Snes9x" / "snes9x-x64.exe"
if not snes9x_exe.is_file():
    print("Downloading Snes9x to venv...")
    snes9x_dir = snes9x_exe.parent
    snes9x_dir.mkdir(parents=True, exist_ok=True)
    with urllib.request.urlopen(
        f"https://github.com/snes9xgit/snes9x/releases/download/{SNES9X_VER}/snes9x-{SNES9X_VER}-win32-x64.zip"
    ) as r, zipfile.ZipFile(
        io.BytesIO(r.read())
    ) as zip:
        zip.extractall(snes9x_dir)

settings = {
    "CoilsnakeCLIPath": str(coilsnake_cli_exe),
    "InputROMPath": str(rom),
    "OutputROMPath": str(here / "Output" / "EarthBound_Mod_Menu.smc"),
    "EmulatorPath": str(snes9x_exe),
    "LaunchEmuAfterBuild": True,
}

tgt_config = here / "Builder" / "bin" / "Debug" / "config.json"
tgt_config.parent.mkdir(parents=True, exist_ok=True)
with open(tgt_config, "w", encoding="utf8") as f:
    json.dump(settings, fp=f, indent=2)

print(f"Done. Please open {here / 'Builder' / 'EBModMenu.sln'} in Visual Studio.")