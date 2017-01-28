using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Snowfall.Graphics
{
    public class ShaderProgram : IDisposable
    {
        public ShaderProgram(string vertex, string fragment)
        {
            ProgramId = GL.CreateProgram();
            Bind();
            int vid = GL.CreateShader(ShaderType.VertexShader);
            int fid = GL.CreateShader(ShaderType.FragmentShader);

            GL.ShaderSource(vid, vertex);
            GL.ShaderSource(fid, fragment);
            
            GL.CompileShader(vid);
            Snowfall.Log(GL.GetShaderInfoLog(vid));
            int vstatus; GL.GetShader(vid, ShaderParameter.CompileStatus, out vstatus);
            if (vstatus != 1)
            {
                Snowfall.Log("Vertex shader compilation failed!");
                throw new Exception("Vertex shader could not compile! This event has been logged.");
            }

            GL.CompileShader(fid);
            Snowfall.Log(GL.GetShaderInfoLog(vid));
            int fstatus; GL.GetShader(fid, ShaderParameter.CompileStatus, out fstatus);
            if (fstatus != 1)
            {
                Snowfall.Log("Fragment shader compilation failed!");
                throw new Exception("Fragment shader could not compile! This event has been logged.");
            }

            GL.AttachShader(ProgramId, vid);
            GL.AttachShader(ProgramId, fid);
            GL.LinkProgram(ProgramId);
            GL.DetachShader(ProgramId, vid);
            GL.DetachShader(ProgramId, fid);
            GL.DeleteShader(vid);
            GL.DeleteShader(fid);
        }

        public ShaderProgram(string vertex, string fragment, string geometry)
        {
            ProgramId = GL.CreateProgram();
            Bind();

            int vid = GL.CreateShader(ShaderType.VertexShader);
            int fid = GL.CreateShader(ShaderType.FragmentShader);
            int gid = GL.CreateShader(ShaderType.GeometryShader);

            GL.ShaderSource(vid, vertex);
            GL.ShaderSource(fid, fragment);
            GL.ShaderSource(gid, geometry);

            GL.CompileShader(vid);
            Snowfall.Log(GL.GetShaderInfoLog(vid));
            int vstatus; GL.GetShader(vid, ShaderParameter.CompileStatus, out vstatus);
            if (vstatus != 1)
            {
                Snowfall.Log("Vertex shader compilation failed!");
                throw new Exception("Vertex shader could not compile! This event has been logged.");
            }

            GL.CompileShader(fid);
            Snowfall.Log(GL.GetShaderInfoLog(vid));
            int fstatus; GL.GetShader(fid, ShaderParameter.CompileStatus, out fstatus);
            if (fstatus != 1)
            {
                Snowfall.Log("Fragment shader compilation failed!");
                throw new Exception("Fragment shader could not compile! This event has been logged.");
            }

            GL.CompileShader(gid);
            Snowfall.Log(GL.GetShaderInfoLog(gid));
            int gstatus; GL.GetShader(gid, ShaderParameter.CompileStatus, out gstatus);
            if (gstatus != 1)
            {
                Snowfall.Log("Geometry shader compilation failed!");
                throw new Exception("Geometry shader could not compile! This event has been logged.");
            }

            GL.AttachShader(ProgramId, vid);
            GL.AttachShader(ProgramId, fid);
            GL.AttachShader(ProgramId, gid);

            GL.LinkProgram(ProgramId);

            GL.DetachShader(ProgramId, vid);
            GL.DetachShader(ProgramId, fid);
            GL.DeleteShader(vid);
            GL.DeleteShader(fid);
        }
        #region Uniform Properties
        public void SetUniform(string name, int value)
        {
            int index = GL.GetUniformLocation(ProgramId, name);
            GL.Uniform1(index, value);
        }

        public void SetUniform(string name, float value)
        {
            int index = GL.GetUniformLocation(ProgramId, name);
            GL.Uniform1(index, value);
        }

        public void SetUniform(string name, double value)
        {
            int index = GL.GetUniformLocation(ProgramId, name);
            GL.Uniform1(index, value);
        }

        public void SetUniform(string name, Vector2 value)
        {
            int index = GL.GetUniformLocation(ProgramId, name);
            GL.Uniform2(index, value);
        }

        public void SetUniform(string name, Vector3 value)
        {
            int index = GL.GetUniformLocation(ProgramId, name);
            GL.Uniform3(index, value);
        }

        public void SetUniform(string name, Vector4 value)
        {
            int index = GL.GetUniformLocation(ProgramId, name);
            GL.Uniform4(index, value);
        }

        public void SetUniform(string name, Matrix2 value)
        {
            int index = GL.GetUniformLocation(ProgramId, name);
            GL.UniformMatrix2(index, false, ref value);
        }

        public void SetUniform(string name, Matrix3 value)
        {
            int index = GL.GetUniformLocation(ProgramId, name);
            GL.UniformMatrix3(index, false, ref value);
        }
        #endregion

        public void Bind()
        {
            GL.UseProgram(ProgramId);
        }

        public void Dispose()
        {
            if (ProgramId == -2)
            {
                Snowfall.Log("Duplicate dispose of shader program!");
                return;
            }
            GL.DeleteProgram(ProgramId);
            ProgramId = -2;
        }

        ~ShaderProgram()
        {
            if (ProgramId != -2)
                Snowfall.Log("Shader program not disposed!");
        }

        public string ProgramLog { get { return GL.GetProgramInfoLog(ProgramId); } }
        public int ProgramId { get; private set; }
    }
}
